namespace HappyMe.Web.Models.Areas.Administration.InputModels.Questions
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Data.Models;

    using HappyMe.Common.Mapping;

    public class QuestionUpdateInputModel : QuestionCreateInputModel, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<QuestionUpdateInputModel, Question>()
                .ForMember(m => m.AuthorId, opt => opt.Ignore());

            configuration.CreateMap<Question, QuestionUpdateInputModel>()
                .ForMember(m => m.ModulesIds, opt => opt.MapFrom(x => x.Modules.Select(y => y.Id)));
        }
    }
}