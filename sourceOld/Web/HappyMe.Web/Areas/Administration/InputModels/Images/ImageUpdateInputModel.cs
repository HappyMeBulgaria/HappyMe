namespace HappyMe.Web.Areas.Administration.InputModels.Images
{
    using System.Web.Mvc;

    using HappyMe.Common.Mapping;

    public class ImageUpdateInputModel : ImageCreateInputModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}