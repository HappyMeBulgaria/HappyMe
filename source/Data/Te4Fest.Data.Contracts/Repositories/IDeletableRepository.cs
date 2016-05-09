// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeletableRepository.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Te4Fest.Data.Contracts.Repositories
{
    using System.Linq;

    public interface IDeletableEntityRepository<T> : IRepository<T>
        where T : class
    {
        IQueryable<T> AllWithDeleted();

        void HardDelete(T entity);

        void HardDelete(params object[] id);
    }
}
