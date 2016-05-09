namespace Te4Fest.Services.Administration.Base
{
    using System.Linq;

    using Te4Fest.Data.Contracts.Models;
    using Te4Fest.Data.Contracts.Repositories;
    using Te4Fest.Services.Administration.Contracts;

    public class DeletableEntityAdministrationService<TDeletableEntity> :
            AdministrationService<TDeletableEntity>,
            IDeletableEntityAdministrationService<TDeletableEntity>
        where TDeletableEntity : class, IDeletableEntity
    {
        public DeletableEntityAdministrationService(IDeletableEntityRepository<TDeletableEntity> entities)
            : base(entities)
        {
            this.Entities = entities;
        }

        protected new IDeletableEntityRepository<TDeletableEntity> Entities { get; }

        public virtual IQueryable<TDeletableEntity> ReadWithDeleted()
        {
            return this.Entities.AllWithDeleted();
        }

        public virtual void HardDelete(params object[] id)
        {
            this.Entities.HardDelete(id);
            this.Entities.SaveChanges();
        }

        public void HardDelete(TDeletableEntity entity)
        {
            this.Entities.HardDelete(entity);
            this.Entities.SaveChanges();
        }
    }
}
