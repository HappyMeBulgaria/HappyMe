namespace HappyMe.Data.Contracts.Repositories.Contracts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> All();

        T GetById(params object[] id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(params object[] id);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
