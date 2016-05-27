namespace HappyMe.Services.Administration
{
    using System.Linq;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Base;
    using HappyMe.Services.Administration.Contracts;

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
