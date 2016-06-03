﻿namespace HappyMe.Web.Areas.Administration.InputModels.Answers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;
    using Data.Models;
    using HappyMe.Common.Mapping;

    public class AnswerCreateInputModel : IMapTo<Answer>, IMapFrom<Answer>
    {
        [UIHint("DropDownList")]
        [Display(Name = "Въпрос")]
        public int QuestionId { get; set; }

        [AllowHtml]
        [UIHint("CKeditor")]
        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public bool IsHidden { get; set; }

        public int OrderBy { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ImageId { get; set; }

        [UIHint("ImageUpload")]
        [Display(Name = "Изображение")]
        public HttpPostedFileBase ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }
    }
}