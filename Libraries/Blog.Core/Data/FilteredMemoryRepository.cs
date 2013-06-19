using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Data
{
    public abstract class FilteredMemoryRepository<TEntity, TFilterData> : MemoryRepository<TEntity>, IFilteredRepository<TEntity, TFilterData>
        where TEntity : class, IEntity
        where TFilterData : class
    {
        public List<TEntity> GetAll(TFilterData filterData)
        {
            return GetFilteredItems(Data.Values, filterData).ToList();
        }

        public List<TEntity> GetAll(string sortExpression, TFilterData filterData)
        {
            var result = GetSortedItems(Data.Values.ToList(), sortExpression);

            if (filterData != null)
                result = GetFilteredItems(result, filterData);

            return result.ToList();
        }

        public int GetTotalCount(TFilterData filterData)
        {
            return GetFilteredItems(Data.Values, filterData).Count();
        }

        public IEnumerable<TEntity> GetPage(int startPage, int pageSize, TFilterData filterData)
        {
            var startRow = pageSize * (startPage - 1);

            return GetPage(startRow, pageSize, string.Empty, filterData);
        }

        public IEnumerable<TEntity> GetPage(int startRowNumber, int pageSize, string sortExpression, TFilterData filterData)
        {
            var result = GetSortedItems(Data.Values.ToList(), sortExpression);

            if (filterData != null)
                result = GetFilteredItems(result, filterData);

            result = result.Skip(startRowNumber).Take(pageSize);

            return result;
        }

        public void Delete(TFilterData filterData)
        {
            IEnumerable<TEntity> items = null;

            if (filterData != null)
                items = GetFilteredItems(Data.Values, filterData);

            foreach (var entity in items)
            {
                Delete(entity.Id);
            }
        }

        private IEnumerable<TEntity> GetSortedItems(List<TEntity> source, string sortExpression)
        {
            var result = source;

            if (!string.IsNullOrEmpty(sortExpression))
            {
                var sortingProvider = new PropertyCompareProvider<TEntity>
                {
                    SortKey = sortExpression
                };

                result.Sort(sortingProvider);
            }

            return result;
        }

        protected abstract IEnumerable<TEntity> GetFilteredItems(IEnumerable<TEntity> source, TFilterData filterData);
    }
}