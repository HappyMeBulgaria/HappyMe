namespace HappyMe.Data.Contracts.Repositories
{
    using System;
    using System.Linq;

    using HappyMe.Data.Contracts.Models;
    using HappyMe.Data.Contracts.Repositories.Contracts;

    public class InMemoryDeletableEntityRepository<T, TKey> : InMemoryRepository<T, TKey>, IDeletableEntityRepository<T>
        where T : class, IIdentifiable<TKey>, IDeletableEntity
    {
        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public override void Delete(T entity)
        {
            var databaseEntity = this.DatabaseStore.FirstOrDefault(x => x.Id.Equals(entity.Id));
            if (databaseEntity != null)
            {
                databaseEntity.IsDeleted = true;
                databaseEntity.DeletedOn = DateTime.Now;
            }
        }

        public override void Delete(params object[] id)
        {
            var databaseEntity = this.GetById(id);
            this.Delete(databaseEntity);
        }

        public void HardDelete(T entity)
        {
            base.Delete(entity);
        }

        public void HardDelete(params object[] id)
        {
            base.Delete(id);
        }
    }
}
