using System.Collections.Generic;

namespace Blog.Core.Data
{
    public interface IFilteredRepository<TEntity, TFilterData> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TFilterData : class
    {
        List<TEntity> GetAll(TFilterData filterData);

        List<TEntity> GetAll(string sortExpression, TFilterData filterData);

        int GetTotalCount(TFilterData filterData);

        IEnumerable<TEntity> GetPage(int startRowNumber, int pageSize, string sortExpression, TFilterData filterData);
        IEnumerable<TEntity> GetPage(int startPage, int pageSize, TFilterData filterData);

        void Delete(TFilterData filterData);
    }
}