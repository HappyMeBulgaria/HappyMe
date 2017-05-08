﻿namespace HappyMe.Web.Areas.Administration.InputModels.Modules
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class ModuleUpdateInputModel : IMapTo<Module>, IMapFrom<Module>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Име на модул")]
        ////[PlaceHolder("Име на модул")]
        public string Name { get; set; }

        [Display(Name = "Описание на модул")]
        [UIHint("CKeditor")]
        ////[PlaceHolder("Описание на модул")]
        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string AuthorId { get; set; }

        [Display(Name = "Активен?")]
        public bool IsActive { get; set; }

        [Display(Name = "Публичен?")]
        public bool IsPublic { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? ImageId { get; set; }

        [Display(Name = "Изображение")]
        [UIHint("ImageUpload")]
        public IFormFile ImageFile { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ModuleUpdateInputModel, Module>()
                .ForMember(m => m.AuthorId, opt => opt.Ignore());
        }
    }
}