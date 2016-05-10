namespace Te4Fest.Services.Common.Mapping
{
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Te4Fest.Services.Common.Mapping.Contracts;

    public class AutoMapperMappingService : IMappingService
    {
        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        public IQueryable<TDestination> MapCollection<TDestination>(IQueryable source, object parameters = null)
        {
            return source.ProjectTo<TDestination>(parameters);
        }
    }
}
