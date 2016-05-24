namespace Te4Fest.Web.Areas.Administration.InputModels.Images
{
    using System.Web.Mvc;

    using Te4Fest.Common.Mapping;

    public class ImageUpdateInputModel : ImageCreateInputModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}