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

        public IQueryable<Module> AllWithQuestions() => this.All().Where(x => x.Questions.Any());

        public IQueryable<Module> AllActive() => this.modulesRepository.All().Where(m => m.IsActive);

        public IQueryable<Module> AllActiveWithQuestions() => this.AllActive().Where(x => x.Questions.Any());

        public IQueryable<Module> AllPublic() => this.modulesRepository.All().Where(m => m.IsPublic);

        public IQueryable<Module> AllPublicWithQuestions() => this.AllPublic().Where(x => x.Questions.Any());

        public Module GetById(int id) => this.modulesRepository.GetById(id);
    }
}
