namespace HappyMe.Web.Models.Areas.Administration.InputModels.Questions
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using Common.Attributes;

    using Data.Models;

    using HappyMe.Common.Mapping;
    using HappyMe.Common.Models;

    public class QuestionCreateInputModel : IMapTo<Question>, IMapFrom<Question>
    {
        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }

        [Display(Name = "Име на въпроса")]
        [PlaceHolder("Име на въпроса")]
        public string Text { get; set; }

        [UIHint("EnumDropDownList")]
        public QuestionType Type { get; set; }

        [Display(Name = "Публичен?")]
        public bool IsPublic { get; set; }

        ////[Display(Name = "Присъства в модул")]
        ////[UIHint("DropDownList")]
        ////public int ModuleId { get; set; }

        [Display(Name = "Присъства в модули")]
        [UIHint("MultiSelectDropDownList")]
        public int[] ModulesIds { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ImageId { get; set; }

        [UIHint("ImageUpload")]
        [Display(Name = "Изображение")]
        public HttpPostedFileBase ImageData { get; set; }
    }
}