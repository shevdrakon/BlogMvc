namespace Blog.Web.Framework.Mappers
{
    public class Mapper : IMapper
    {
        public T Map<T>(object source)
        {
            return AutoMapper.Mapper.Map<T>(source);
        }

        public void RegisterMap<TSource, TDestination>()
        {
            AutoMapper.Mapper.CreateMap<TSource, TDestination>();
        }
    }
}