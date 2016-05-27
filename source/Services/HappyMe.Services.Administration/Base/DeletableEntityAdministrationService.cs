namespace HappyMe.Services.Administration.Base
{
    using System.Linq;

    using HappyMe.Data.Contracts.Models;
    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Services.Administration.Contracts;

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
