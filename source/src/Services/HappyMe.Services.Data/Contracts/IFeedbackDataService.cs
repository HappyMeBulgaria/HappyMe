namespace HappyMe.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using HappyMe.Services.Common;

    public interface IFeedbackDataService : IService
    {
        Task Add(string name, string email, string subject, string message);
    }
}
