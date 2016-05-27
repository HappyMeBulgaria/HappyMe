namespace HappyMe.Web.Areas.Administration.ViewModels.Images
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class ImageGridViewModel : IMapFrom<Image>
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Path")]
        public string Path { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageUrl => this.ImageData != null ? string.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String(this.ImageData)) : string.Empty;
    }
}