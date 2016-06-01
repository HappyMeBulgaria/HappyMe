namespace HappyMe.Web.Areas.Administration.ViewModels.Answers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Data.Contracts;
    using Data.Models;
    using HappyMe.Common.Mapping;
    using HappyMe.Common.Models;

    public class AnswerGridViewModel : IMapFrom<Answer>, IMapTo<Answer>, IIdentifiable<int>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        ////[HiddenInput(DisplayValue = false)]
        public int? QuestionId { get; set; }

        public bool IsCorrect { get; set; }

        [AllowHtml]
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