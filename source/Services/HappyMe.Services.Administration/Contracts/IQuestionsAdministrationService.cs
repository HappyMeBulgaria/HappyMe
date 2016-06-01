using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMe.Services.Administration.Contracts
{
    using Data.Models;

    public interface IQuestionsAdministrationService : IAdministrationService<Question>
    {
        IQueryable<Question> GetAllOrderedQuestions();

        IQueryable<Question> GetUserQuestions(string userId);

        IQueryable<Question> GetUserAndPublicQuestions(string userId);
    }
}