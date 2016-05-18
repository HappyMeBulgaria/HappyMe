namespace Te4Fest.Web.Areas.Administration.ViewModels.Images
{
    using System.ComponentModel.DataAnnotations;

    using Te4Fest.Common.Mapping;
    using Te4Fest.Data.Models;

    public class ImageGridViewModel : IMapFrom<Image>
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Path")]
        public string Path { get; set; }

        ////public string AuthorId { get; set; }
    }
}