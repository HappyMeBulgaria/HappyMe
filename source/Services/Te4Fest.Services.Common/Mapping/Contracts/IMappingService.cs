namespace Te4Fest.Services.Common.Mapping.Contracts
{
    using System.Linq;

    using Te4Fest.Services.Common;

    public interface IMappingService : IService
    {
        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        IQueryable<TDestination> MapCollection<TDestination>(IQueryable source, object parameters = null);
    }
}
