namespace HappyMe.Services.Administration
{
    using System;
    using System.Linq;
    using Base;
    using Contracts;
    using Data.Contracts.Repositories;
    using Data.Models;

    public class QuestionsAdministrationService : AdministrationService<Question>, IQuestionsAdministrationService
    {
        public QuestionsAdministrationService(IRepository<Question> entities) : base(entities)
        {
        }

        public IQueryable<Question> GetAllOrderedQuestions()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Question> GetUserQuestions(string userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Question> GetUserAndPublicQuestions(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
