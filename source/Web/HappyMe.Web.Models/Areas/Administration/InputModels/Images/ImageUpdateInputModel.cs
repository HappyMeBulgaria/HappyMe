﻿namespace HappyMe.Web.Models.Areas.Administration.InputModels.Images
{
    using System.Web.Mvc;

    public class ImageUpdateInputModel : ImageCreateInputModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}