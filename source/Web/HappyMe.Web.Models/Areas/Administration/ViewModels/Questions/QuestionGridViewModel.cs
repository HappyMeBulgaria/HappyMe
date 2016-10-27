namespace HappyMe.Web.Models.Areas.Administration.ViewModels.Questions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Data.Contracts;
    using Data.Models;

    using HappyMe.Common.Mapping;
    using HappyMe.Common.Models;

    public class QuestionGridViewModel : IMapFrom<Question>, IMapTo<Question>, IIdentifiable<int>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [AllowHtml]
        [UIHint("CKeditor")]
        public string Text { get; set; }

        public QuestionType Type { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ModuleId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }

        public string ImageUrl => this.Image != null ?
            $"data:image/jpeg;base64,{Convert.ToBase64String(this.Image.ImageData)}"
            : string.Empty;

        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }
    }
}