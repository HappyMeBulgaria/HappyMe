namespace HappyMe.Services.Administration.Base
{
    using System.Linq;

    using HappyMe.Data.Contracts;
    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Services.Administration.Contracts;

    public class AdministrationService<TEntity> : IAdministrationService<TEntity>
        where TEntity : class, IEntity
    {
        public AdministrationService(IRepository<TEntity> entities)
        {
            this.Entities = entities;
        }

        protected IRepository<TEntity> Entities { get; }

        public virtual IQueryable<TEntity> Read()
        {
            return this.Entities.All();
        }

        public virtual TEntity Get(params object[] id)
        {
            return this.Entities.GetById(id);
        }

        public virtual void Create(TEntity entity)
        {
            this.Entities.Add(entity);
            this.Entities.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            this.Entities.Update(entity);
            this.Entities.SaveChanges();
        }

        public virtual void Delete(params object[] id)
        {
            this.Entities.Delete(id);
            this.Entities.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            this.Entities.Delete(entity);
            this.Entities.SaveChanges();
        }
    }
}
