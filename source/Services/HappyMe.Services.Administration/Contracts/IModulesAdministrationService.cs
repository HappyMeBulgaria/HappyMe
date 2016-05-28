namespace HappyMe.Services.Administration.Contracts
{
    using System.Linq;

    using HappyMe.Data.Models;

    public interface IModulesAdministrationService : IAdministrationService<Module>
    {
        IQueryable<Module> GetAllOrderedModules();

        IQueryable<Module> GetUserModules(string userId);

        IQueryable<Module> GetUserAndPublicModules(string userId);
    }
}
