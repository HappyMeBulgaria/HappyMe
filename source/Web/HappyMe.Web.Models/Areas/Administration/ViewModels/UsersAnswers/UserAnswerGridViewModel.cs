namespace HappyMe.Web.Models.Areas.Administration.ViewModels.UsersAnswers
{
    using System;

    using AutoMapper;

    using Data.Models;

    using HappyMe.Common.Mapping;

    public class UserAnswerGridViewModel : IMapFrom<UserAnswer>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string AnswerText { get; set; }

        public string UserId { get; set; }

        public int AnswerId { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<UserAnswer, UserAnswerGridViewModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(e => e.User.UserName));
        }
    }
}