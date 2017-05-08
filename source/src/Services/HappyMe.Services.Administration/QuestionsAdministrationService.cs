namespace HappyMe.Services.Administration
{
    using System.Linq;

    using Base;
    using Contracts;
    using Data.Models;

    using HappyMe.Data.Contracts.Repositories.Contracts;

    public class QuestionsAdministrationService : AdministrationService<Question>, IQuestionsAdministrationService
    {
        public QuestionsAdministrationService(IRepository<Question> entities) 
            : base(entities)
        {
        }

        public IQueryable<Question> GetUserQuestions(string userId)
        {
            return this.Read().Where(q => q.AuthorId == userId);
        }

        public IQueryable<Question> GetUserAndPublicQuestions(string userId) => 
            this.Read().Where(q => q.AuthorId == userId || q.IsPublic);

        public bool CheckIfUserIsAuthorOnQuestion(string userId, int questionId) => 
            this.Read().Any(q => q.AuthorId == userId && q.Id == questionId);
    }
}
