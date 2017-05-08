namespace HappyMe.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Common.Models;
    using HappyMe.Data.Models;

    public class QuestionViewModel : IMapFrom<Question>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public QuestionType Type { get; set; }

        public byte[] ImageData { get; set; }

        public int SessionId { get; set; }

        public string ImageUrl => this.ImageData != null ?
            $"data:image/jpeg;base64,{Convert.ToBase64String(this.ImageData)}"
            : string.Empty;

        public IEnumerable<AnswerViewModel> Answers { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Question, QuestionViewModel>()
                .ForMember(m => m.ImageData, opt => opt.MapFrom(e => e.Image.ImageData));
        }
    }
}