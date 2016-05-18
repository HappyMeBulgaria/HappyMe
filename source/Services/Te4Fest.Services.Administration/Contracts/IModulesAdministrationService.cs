namespace Te4Fest.Services.Administration.Contracts
{
    using System.Linq;

    using Te4Fest.Data.Models;

    public interface IModulesAdministrationService : IAdministrationService<Module>
    {
        IQueryable<Module> GetAllOrderedModules();
    }
}
