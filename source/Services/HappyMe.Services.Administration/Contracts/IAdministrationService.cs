namespace HappyMe.Services.Administration.Contracts
{
    using System.Linq;
    using HappyMe.Services.Common;

    public interface IAdministrationService<TEntity> : IService
        where TEntity : class
    {
        IQueryable<TEntity> Read();

        TEntity Get(params object[] id);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(params object[] id);

        void Delete(TEntity entity);
    }
}
