namespace HappyMe.Services.Administration
{
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Base;
    using HappyMe.Services.Administration.Contracts;

    public class ImagesAdministrationService : AdministrationService<Image>, IImagesAdministrationService
    {
        public ImagesAdministrationService(IRepository<Image> entities)
            : base(entities)
        {
        }

        public Image Create(byte[] imageByteArray, string authorId)
        {
            var image = new Image
            {
                ImageData = imageByteArray,
                AuthorId = authorId
            };

            this.Entities.Add(image);
            this.Entities.SaveChanges();

            return image;
        }

        public Image Update(byte[] imageByteArray, int? imageId, string authorId, bool isAdmin)
        {
            if (!imageId.HasValue)
            {
                return this.Create(imageByteArray, authorId);
            }

            var existingImage = this.Entities.GetById(imageId);

            if (existingImage == null)
            {
                return this.Create(imageByteArray, authorId);
            }

            if (existingImage.AuthorId != authorId && !isAdmin)
            {
                return this.Create(imageByteArray, authorId);
            }

            existingImage.ImageData = imageByteArray;
            this.Entities.Update(existingImage);
            this.Entities.SaveChanges();

            return existingImage;
        }
    }
}