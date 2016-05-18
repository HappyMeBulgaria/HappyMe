namespace Te4Fest.Services.Data.Contracts
{
    using System.Linq;

    using Te4Fest.Data.Models;
    using Te4Fest.Services.Common;

    public interface IModulesDataService : IService
    {
        IQueryable<Module> GetAllModules();

        IQueryable<Module> GetAllActiveModules();
    }
}
