namespace HappyMe.Web.Models.Areas.Administration.ViewModels.Images
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    using HappyMe.Common.Mapping;

    public class ImageGridViewModel : IMapFrom<Image>
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Path")]
        public string Path { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageUrl => this.ImageData != null ?
            $"data:image/jpeg;base64,{Convert.ToBase64String(this.ImageData)}"
            : string.Empty;
    }
}