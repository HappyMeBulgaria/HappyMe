namespace HappyMe.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyMe.Common.Tools;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    public class ModuleSessionDataService : IModuleSessionDataService
    {
        private readonly IRepository<ModuleSession> moduleSessionsRepository;
        private readonly IRepository<UserAnswer> userAnswersRepository;
        private readonly IRepository<Module> modulesRepository;

        public ModuleSessionDataService(
            IRepository<ModuleSession> moduleSessionsRepository,
            IRepository<UserAnswer> userAnswersRepository,
            IRepository<Module> modulesRepository)
        {
            this.moduleSessionsRepository = moduleSessionsRepository;
            this.userAnswersRepository = userAnswersRepository;
            this.modulesRepository = modulesRepository;
        }

        public ModuleSession GetById(int id)
        {
            return this.moduleSessionsRepository.GetById(id);
        }

        public Question NextQuestion(int moduleSessionId, string userId)
        {
            var answerdQuestioIds =
                this.userAnswersRepository.All()
                    .Where(x => x.ModuleSessionId == moduleSessionId && x.UserId == userId && x.Answer.IsCorrect)
                    .Select(x => x.Answer.QuestionId);

            var unanswerdQuestions =
                this.moduleSessionsRepository
                    .GetById(moduleSessionId)
                    .Module.Questions.Where(x => !answerdQuestioIds.Contains(x.Id))
                    .ToArray();

            if (!unanswerdQuestions.Any())
            {
                this.moduleSessionsRepository.GetById(moduleSessionId).IsFinised = true;
                this.moduleSessionsRepository.SaveChanges();
                return null;
            }

            var selectedQuestion = this.OneOf(RandomGenerator.Instance, unanswerdQuestions);

            selectedQuestion.Answers = this.Shuffle(selectedQuestion.Answers, RandomGenerator.Instance)
                .ToList();

            return selectedQuestion;
        }

        public void FinishSession(int id)
        {
            var session = this.moduleSessionsRepository.GetById(id);
            if (session == null)
            {
                throw new InvalidOperationException("No such session exists.");
            }

            session.IsFinised = true;
            session.FinishDate = DateTime.Now;
            this.moduleSessionsRepository.SaveChanges();
        }

        public async Task<ModuleSession> StartAnonymousSession(int moduleId)
        {
            var moduleExists = this.modulesRepository.All().Any(x => x.Id == moduleId);

            if (!moduleExists)
            {
                throw new InvalidOperationException("No such module exists");
            }

            var newSession = new ModuleSession(moduleId)
            {
                StartedDate = DateTime.Now
            };
            this.moduleSessionsRepository.Add(newSession);
            await this.moduleSessionsRepository.SaveChangesAsync();

            return newSession;
        }

        public async Task<ModuleSession> StartUserSession(string userId, int moduleId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId), "UserId must have a value");
            }

            var moduleExists = this.modulesRepository.All().Any(x => x.Id == moduleId);

            if (!moduleExists)
            {
                throw new InvalidOperationException("No such module exists");
            }

            var newSession = new ModuleSession(userId, moduleId)
            {
                StartedDate = DateTime.Now
            };
            this.moduleSessionsRepository.Add(newSession);
            await this.moduleSessionsRepository.SaveChangesAsync();

            return newSession;
        }

        private T OneOf<T>(Random random, IList<T> items)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            int index = random.Next(0, items.Count);
            return items.ElementAt(index);
        }

        private IEnumerable<T> Shuffle<T>(IEnumerable<T> items, Random randomGenerator)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (randomGenerator == null)
            {
                throw new ArgumentNullException(nameof(randomGenerator));
            }

            return this.ShuffleIterator(items, randomGenerator);
        }

        private IEnumerable<T> ShuffleIterator<T>(IEnumerable<T> items, Random randomGenerator)
        {
            var buffer = items.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                var j = randomGenerator.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}
