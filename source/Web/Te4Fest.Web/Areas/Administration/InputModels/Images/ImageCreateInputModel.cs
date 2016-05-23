namespace Te4Fest.Web.Areas.Administration.InputModels.Images
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Web;

    using AutoMapper;

    using MoreDotNet.Extentions.Common;

    using Te4Fest.Common.Mapping;
    using Te4Fest.Data.Models;

    public class ImageCreateInputModel : IMapFrom<Image>, IMapTo<Image>, IHaveCustomMappings
    {
        [Required(ErrorMessage = "Моля изберете изображение.")]
        public HttpPostedFileBase ImageFile { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration
                .CreateMap<ImageCreateInputModel, Image>("ImageCreateInputModel")
                .ForMember(
                x => x.ImageData, 
                x => x.MapFrom(y => y.ImageFile.InputStream.ToByteArray()));
        }
    }
}