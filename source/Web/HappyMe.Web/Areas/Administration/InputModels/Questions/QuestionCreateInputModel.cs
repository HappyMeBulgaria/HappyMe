using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HappyMe.Common.Mapping;
using HappyMe.Common.Models;
using HappyMe.Data.Models;
using HappyMe.Web.Areas.Administration.InputModels.Images;
using MoreDotNet.Extentions.Common;

namespace HappyMe.Web.Areas.Administration.InputModels.Questions
{
    public class QuestionCreateInputModel : IMapTo<Question>, IMapFrom<Question>
    {
        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }

        [AllowHtml]
        [UIHint("CKeditor")]
        public string Text { get; set; }

        [UIHint("EnumDropDownList")]
        public QuestionType Type { get; set; }

        public bool IsPublic { get; set; }

        [UIHint("DropDownList")]
        [Display(Name = "Модул")]
        public int ModuleId { get; set; }

        // TODO: uncomment when ImageUpload.cshtml is ready
        [HiddenInput(DisplayValue = false)]
        public int? ImageId { get; set; }

        [UIHint("ImageUpload")]
        [Display(Name = "Изображение")]
        public HttpPostedFileBase ImageData { get; set; }
    }
}