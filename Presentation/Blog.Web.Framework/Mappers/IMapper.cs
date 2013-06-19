namespace Blog.Web.Framework.Mappers
{
    public interface IMapper
    {
        T Map<T>(object source);
    }
}
