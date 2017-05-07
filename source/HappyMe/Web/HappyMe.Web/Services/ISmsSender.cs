using System.Threading.Tasks;

namespace HappyMe.Web.Services
{
    public interface ISmsSender : IService
    {
        Task SendSmsAsync(string number, string message);
    }
}
