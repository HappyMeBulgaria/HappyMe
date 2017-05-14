namespace HappyMe.Data.Contracts.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HappyMe.Data.Contracts.Repositories.Contracts;

    public class InMemoryRepository<T, TKey> : IRepository<T>
        where T : class, IIdentifiable<TKey>
    {
        private readonly IList<T> databaseStore;

        public InMemoryRepository()
        {
            this.databaseStore = new List<T>();
        }

        protected IList<T> DatabaseStore => this.databaseStore;

        public void Add(T entity)
        {
            this.databaseStore.Add(entity);
        }

        public virtual IQueryable<T> All()
        {
            return this.databaseStore.AsQueryable();
        }

        public virtual void Delete(params object[] id)
        {
            ////this.DatabaseStore.RemoveAll(x => id.Any(y => x.Id.Equals(y)));
        }

        public virtual void Delete(T entity)
        {
            this.databaseStore.Remove(entity);
        }

        public void Dispose()
        {
        }

        public T GetById(params object[] id)
        {
            return this.databaseStore.FirstOrDefault(x => id.Any(y => x.Id.Equals(y)));
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
