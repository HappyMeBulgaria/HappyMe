namespace HappyMe.Services.Data
{
    using System.Linq;

    using HappyMe.Data.Contracts.Repositories.Contracts;
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

        public IQueryable<Module> AllPublicWithQuestionsWithCorrectAnswer() =>
            this.AllPublic().Where(x => x.Questions.Any(q => q.Answers.Any(a => a.IsCorrect)));

        public Module GetById(int id) => this.modulesRepository.GetById(id);
    }
}
