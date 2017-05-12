namespace HappyMe.Web.Areas.Administration.InputModels.Images
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;
    using HappyMe.Web.Common.Extensions;

    using Microsoft.AspNetCore.Http;

    public class ImageCreateInputModel : IMapFrom<Image>, IMapTo<Image>, IHaveCustomMappings
    {
        [Required(ErrorMessage = "Моля изберете изображение.")]
        [UIHint("ImageUpload")]
        public IFormFile ImageFile { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration
                .CreateMap<ImageCreateInputModel, Image>()
                .ForMember(
                x => x.ImageData,
                x => x.MapFrom(y => y.ImageFile.ToByteArray()));
        }
    }
}