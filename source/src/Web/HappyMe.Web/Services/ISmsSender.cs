using System.Threading.Tasks;

namespace HappyMe.Web.Services
{
    using HappyMe.Services.Common;

    public interface ISmsSender : IService
    {
        Task SendSmsAsync(string number, string message);
    }
}
