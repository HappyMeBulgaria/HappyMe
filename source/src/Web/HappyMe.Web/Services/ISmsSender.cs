namespace HappyMe.Web.Services
{
    using System.Threading.Tasks;

    using HappyMe.Services.Common;

    public interface ISmsSender : IService
    {
        Task SendSmsAsync(string number, string message);
    }
}
