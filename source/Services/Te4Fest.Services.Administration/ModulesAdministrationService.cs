namespace Te4Fest.Services.Administration
{
    using System.Linq;

    using Te4Fest.Data.Contracts.Repositories;
    using Te4Fest.Data.Models;
    using Te4Fest.Services.Administration.Base;
    using Te4Fest.Services.Administration.Contracts;

    public class ModulesAdministrationService : AdministrationService<Module>, IModulesAdministrationService
    {
        public ModulesAdministrationService(IRepository<Module> entities) : base(entities)
        {
        }

        public IQueryable<Module> GetAllOrderedModules()
        {
            return this.Read().OrderBy(m => m.CreatedOn);
        }
    }
}
