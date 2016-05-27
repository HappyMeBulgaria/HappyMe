namespace HappyMe.Web.Areas.Administration.InputModels.Images
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Web;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    using MoreDotNet.Extentions.Common;

    public class ImageCreateInputModel : IMapFrom<Image>, IMapTo<Image>, IHaveCustomMappings
    {
        [Required(ErrorMessage = "Моля изберете изображение.")]
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