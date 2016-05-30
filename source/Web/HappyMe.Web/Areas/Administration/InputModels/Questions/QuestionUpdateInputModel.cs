using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HappyMe.Common.Mapping;
using HappyMe.Data.Models;
using HappyMe.Web.Areas.Administration.InputModels.Modules;

namespace HappyMe.Web.Areas.Administration.InputModels.Questions
{
    public class QuestionUpdateInputModel : QuestionCreateInputModel, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<QuestionUpdateInputModel, Question>()
                .ForMember(m => m.AuthorId, opt => opt.Ignore());
        }
    }
}