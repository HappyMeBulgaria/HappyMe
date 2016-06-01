using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyMe.Web.Areas.Administration.InputModels.Answers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Data.Models;
    using HappyMe.Common.Mapping;
    using Questions;

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