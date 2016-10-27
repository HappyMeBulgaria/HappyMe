namespace HappyMe.Web.Models.Areas.Administration.InputModels.Images
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using AutoMapper;

    using Data.Models;

    using HappyMe.Common.Mapping;

    using MoreDotNet.Extensions.Common;

    public class ImageCreateInputModel : IMapFrom<Image>, IMapTo<Image>, IHaveCustomMappings
    {
        [Required(ErrorMessage = "Моля изберете изображение.")]
        [UIHint("ImageUpload")]
        public HttpPostedFileBase ImageFile { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration
                .CreateMap<ImageCreateInputModel, Image>("ImageCreateInputModel")
                .ForMember(
                x => x.ImageData, 
                x => x.MapFrom(y => y.ImageFile.InputStream.ToByteArray()));

            configuration
                .CreateMap<ImageUpdateInputModel, Image>("ImageUpdateInputModel")
                .ForMember(
                x => x.ImageData,
                x => x.MapFrom(y => y.ImageFile.InputStream.ToByteArray()));
        }
    }
}