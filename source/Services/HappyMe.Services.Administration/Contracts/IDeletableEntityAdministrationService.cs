namespace HappyMe.Services.Administration.Contracts
{
    using System.Linq;

    using HappyMe.Data.Contracts.Models;

    public interface IDeletableEntityAdministrationService<TDeletableEntity> : IAdministrationService<TDeletableEntity>
        where TDeletableEntity : IDeletableEntity
    {
        IQueryable<TDeletableEntity> ReadWithDeleted();

        void HardDelete(params object[] id);

        void HardDelete(TDeletableEntity entity);
    }
}
