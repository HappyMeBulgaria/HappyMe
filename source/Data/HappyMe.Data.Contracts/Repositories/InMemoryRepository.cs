namespace HappyMe.Data.Contracts.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Data.Contracts.Repositories.Contracts;

    using MoreDotNet.Extensions.Collections;

    public class InMemoryRepository<T, TKey> : IRepository<T>
        where T : class, IIdentifiable<TKey>
    {
        protected readonly IList<T> DatabaseStore;

        public InMemoryRepository()
        {
            this.DatabaseStore = new List<T>();
        }

        public void Add(T entity)
        {
            this.DatabaseStore.Add(entity);
        }

        public virtual IQueryable<T> All()
        {
            return this.DatabaseStore.AsQueryable();
        }

        public virtual void Delete(params object[] id)
        {
            this.DatabaseStore.RemoveAll(x => id.Any(y => x.Id.Equals(y)));
        }

        public virtual void Delete(T entity)
        {
            this.DatabaseStore.Remove(entity);
        }

        public void Dispose()
        {
        }

        public T GetById(params object[] id)
        {
            return this.DatabaseStore.FirstOrDefault(x => id.Any(y => x.Id.Equals(y)));
        }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        public void Update(T entity)
        {
            this.Delete(entity.Id);
            this.Add(entity);
        }
    }
}
