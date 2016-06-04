namespace HappyMe.Web.Areas.Administration.InputModels.Answers
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

        [Display(Name = "Име на въпрос")]
        public string Text { get; set; }

        [Display(Name = "Правилен?")]
        public bool IsCorrect { get; set; }

        [Display(Name = "Публичен?")]
        public bool IsHidden { get; set; }

        [UIHint("Integer")]
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