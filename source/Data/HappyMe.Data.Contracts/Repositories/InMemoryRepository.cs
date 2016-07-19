namespace HappyMe.Data.Contracts.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HappyMe.Data.Contracts.Repositories.Contracts;

    using MoreDotNet.Extentions.Common;

    public class InMemoryRepository<T> : IRepository<T>
        where T : class, IIdentifiable<int>
    {
        private readonly IList<T> databaseStore;

        public InMemoryRepository()
        {
            this.databaseStore = new List<T>();
        }

        public void Add(T entity)
        {
            this.databaseStore.Add(entity);
        }

        public IQueryable<T> All()
        {
            return this.databaseStore.AsQueryable();
        }

        public void Delete(params object[] id)
        {
            this.databaseStore.RemoveAll(x => id.Any(y => x.Id == Convert.ToInt32(y)));
        }

        public void Delete(T entity)
        {
            this.databaseStore.Remove(entity);
        }

        public void Dispose()
        {
        }

        public T GetById(params object[] id)
        {
            return this.databaseStore.FirstOrDefault(x => id.Any(y => x.Id == Convert.ToInt32(y)));
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
