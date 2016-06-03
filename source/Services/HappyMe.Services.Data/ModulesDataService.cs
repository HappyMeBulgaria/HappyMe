namespace HappyMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    public class ModulesDataService : IModulesDataService
    {
        private readonly IRepository<Module> modulesRepository;

        public ModulesDataService(IRepository<Module> modulesRepository)
        {
            this.modulesRepository = modulesRepository;
        }

        public IQueryable<Module> All() => this.modulesRepository.All();

        public IQueryable<Module> AllActive() => this.modulesRepository.All().Where(m => m.IsActive);

        public IQueryable<Module> AllPublic() => this.modulesRepository.All().Where(m => m.IsPublic);

        public Module GetById(int id) => this.modulesRepository.GetById(id);
    }
}
