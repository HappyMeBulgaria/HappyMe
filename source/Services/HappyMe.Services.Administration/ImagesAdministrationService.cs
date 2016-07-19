namespace HappyMe.Services.Administration
{
    using HappyMe.Data.Contracts.Repositories;
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
    }
}