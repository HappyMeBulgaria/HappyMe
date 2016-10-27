namespace HappyMe.Web.Models.Areas.Administration.InputModels.Answers
{
    using System.Web.Mvc;

    using AutoMapper;

    using Data.Models;

    using HappyMe.Common.Mapping;

    public class AnswerUpdateInputModel : AnswerCreateInputModel, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<AnswerUpdateInputModel, Answer>()
                .ForMember(m => m.AuthorId, opt => opt.Ignore());
        }
    }
}