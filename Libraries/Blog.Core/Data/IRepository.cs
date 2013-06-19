namespace Blog.Core.Data
{
    public interface IRepository<T>
        where T : class, IEntity
    {
        T GetById(int id);

        int Insert(T entity);
        
        void Update(T entity);
        
        void Delete(T entity);
        void Delete(int id);
    }
}