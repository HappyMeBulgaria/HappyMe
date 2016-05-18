namespace Te4Fest.Services.Data
{
    using System.Linq;

    using Te4Fest.Data.Contracts.Repositories;
    using Te4Fest.Data.Models;
    using Te4Fest.Services.Data.Contracts;

    public class ModulesDataService : IModulesDataService
    {
        private readonly IDeletableEntityRepository<Module> modulesRepository;

        public ModulesDataService(IDeletableEntityRepository<Module> modulesRepository)
        {
            this.modulesRepository = modulesRepository;
        }

        public IQueryable<Module> GetAllModules() => this.modulesRepository.All();

        public IQueryable<Module> GetAllActiveModules() => this.modulesRepository.All().Where(m => m.IsActive);
    }
}
