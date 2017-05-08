﻿namespace HappyMe.Web.Areas.Administration.InputModels.Answers
{
    using AutoMapper;

    using Data.Models;
    using HappyMe.Common.Mapping;

    using Microsoft.AspNetCore.Mvc;

    public class AnswerUpdateInputModel : AnswerCreateInputModel, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<AnswerUpdateInputModel, Answer>()
                .ForMember(m => m.AuthorId, opt => opt.Ignore());
        }
    }
}