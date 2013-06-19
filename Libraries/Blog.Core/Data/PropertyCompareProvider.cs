using System.Reflection;
using System;
using System.Collections.Generic;

namespace Blog.Core.Data
{
    public class PropertyCompareProvider<TEntity> : IComparer<TEntity>
        where TEntity : class, IEntity
    {
        public string SortKey
        {
            get; 
            set; 
        }

        private string SortProperty
        {
            get
            {
                var result =
                    HasDescSuffix ? SortKey.Substring(0, SortKey.Length - DescSuffix.Length) :
                    HasAscSuffix ? SortKey.Substring(0, SortKey.Length - AscSuffix.Length) :
                    SortKey;

                if (string.IsNullOrEmpty(result))
                    result = "Id";

                return result;
            }
        }

        private bool IsDescSort
        {
            get
            {
                return HasDescSuffix;
            }
        }

        private bool HasDescSuffix
        {
            get
            {
                return SortKey.EndsWith(DescSuffix);
            }
        }

        private bool HasAscSuffix
        {
            get
            {
                return SortKey.EndsWith(AscSuffix);
            }
        }

        private const string DescSuffix = " DESC";
        private const string AscSuffix = " ASC";

        public int Compare(TEntity x, TEntity y)
        {
            var cx = GetPropertyValue(SortProperty, x);
            var cy = GetPropertyValue(SortProperty, y);

            int result;

            if (cx == null && cy == null)
                result = 0;
            else if (cx == null && cy != null)
                result = -1;
            else if (cx != null && cy == null)
                result = 1;
            else
                result = cx.CompareTo(cy);

            if (IsDescSort)
                result = -result;

            return result;
        }

        private IComparable GetPropertyValue(string propertyName, TEntity entity)
        {
            var propertyInfo = entity.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            IComparable result = null;

            if (propertyInfo != null)
                result = propertyInfo.GetValue(entity) as IComparable;

            return result;
        }
    }
}