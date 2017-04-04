namespace HappyMe.Services.Administration
{
    using System.Linq;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Base;
    using HappyMe.Services.Administration.Contracts;

    public class AnswersAdministrationService : AdministrationService<Answer>, IAnswersAdministrationService
    {
        public AnswersAdministrationService(IRepository<Answer> entities) 
            : base(entities)
        {
        }

        public IQueryable<Answer> GetAllUserAnswers(string userId) =>
            this.Read().Where(a => a.AuthorId == userId);

        public bool CheckIfUserIsAnswerAuthor(int answerId, string userId) =>
            this.Read().Any(a => a.Id == answerId && a.AuthorId == userId);
    }
}
