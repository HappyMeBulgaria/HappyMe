namespace HappyMe.Data.Contracts.Repositories
{
    using System;
    using System.Linq;

    using HappyMe.Data.Contracts.Models;
    using HappyMe.Data.Contracts.Repositories.Contracts;

    using Microsoft.EntityFrameworkCore;
    public class EfDeletableEntityRepository<T> : EfRepository<T>, IDeletableEntityRepository<T>
            where T : class, IDeletableEntity
    {
        public EfDeletableEntityRepository(DbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public void HardDelete(T entity)
        {
            base.Delete(entity);
        }

        public void HardDelete(params object[] id)
        {
            base.Delete(id);
        }

        public override void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            this.Update(entity);
        }

        public override void Delete(params object[] id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }
    }
}
