namespace HappyMe.Web.Areas.Administration.ViewModels.Answers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Contracts;
    using Data.Models;
    using HappyMe.Common.Mapping;

    using Microsoft.AspNetCore.Mvc;

    public class AnswerGridViewModel : IMapFrom<Answer>, IMapTo<Answer>, IIdentifiable<int>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        ////[HiddenInput(DisplayValue = false)]
        public int? QuestionId { get; set; }

        public bool IsCorrect { get; set; }

        ////[AllowHtml] Not request validation in ASP.NET Core. Lol
        [UIHint("CKeditor")]
        public string Text { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }

        public string ImageUrl => this.Image != null ? string.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String(this.Image.ImageData)) : string.Empty;

        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }
    }
}