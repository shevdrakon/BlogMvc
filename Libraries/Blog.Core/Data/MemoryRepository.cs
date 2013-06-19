using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Data
{
    public abstract class MemoryRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        protected readonly Dictionary<int, T> Data = new Dictionary<int, T>();

        public T GetById(int id)
        {
            return Data[id];
        }

        public int Insert(T entity)
        {
            if (entity.Id != 0)
                throw new ArgumentException("entity");

            var id = Data.Count + 1;
            entity.Id = id;

            Data.Add(id, entity);

            return id;
        }

        public void Update(T entity)
        {
            if (Data.ContainsKey(entity.Id))
            {
                Data[entity.Id] = entity;
            }
        }

        public void Delete(T entity)
        {
            Delete(entity.Id);
        }

        public void Delete(int id)
        {
            if (Data.ContainsKey(id))
            {
                Data.Remove(id);
            }
        }
    }
}