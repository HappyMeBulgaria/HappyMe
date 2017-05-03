namespace HappyMe.Services.Administration.Contracts
{
    using HappyMe.Data.Models;

    public interface IImagesAdministrationService : IAdministrationService<Image>
    {
        Image Create(byte[] imageByteArray, string authorId);

        Image Update(byte[] imageByteArray, int? imageId, string authorId, bool isAdmin);
    }
}
