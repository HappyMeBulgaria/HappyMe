namespace HappyMe.Web.Areas.Administration.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Common.Models;
    using HappyMe.Data.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Web.Areas.Administration.ViewModels.Answers;

    public class QuestionGridViewModel :
        IMapFrom<Question>,
        IMapTo<Question>,
        IHaveCustomMappings,
        IIdentifiable<int>
    {
        public QuestionGridViewModel()
        {
            this.Answers = new List<AnswerGridViewModel>();
        }

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

        public IEnumerable<AnswerGridViewModel> Answers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Question, QuestionGridViewModel>()
                .ForMember(m => m.Answers, opt => opt.MapFrom(e => e.Answers));
        }
    }
}