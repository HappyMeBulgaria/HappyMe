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

        public IEnumerable<Module> All()
        {
            return this.modulesRepository.All();
        }

        public IEnumerable<Module> AllActive()
        {
            return this.modulesRepository.All().Where(x => x.IsActive);
        }

        public Module GetById(int id)
        {
            return this.modulesRepository.GetById(id);
        }
    }
}
