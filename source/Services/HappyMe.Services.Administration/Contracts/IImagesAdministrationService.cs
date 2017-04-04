using HappyMe.Data.Models;

namespace HappyMe.Services.Administration.Contracts
{
    public interface IImagesAdministrationService : IAdministrationService<Image>
    {
        Image Create(byte[] imageByteArray, string authorId);
    }
}
