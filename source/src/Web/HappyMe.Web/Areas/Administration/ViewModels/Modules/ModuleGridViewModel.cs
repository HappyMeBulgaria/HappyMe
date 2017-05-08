namespace HappyMe.Web.Areas.Administration.ViewModels.Modules
{
    using System;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class ModuleGridViewModel : IMapFrom<Module>, IMapTo<Module>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string Author { get; set; }

        public string AuthorEmail { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageUrl => this.ImageData != null ?
            $"data:image/jpeg;base64,{Convert.ToBase64String(this.ImageData)}"
            : string.Empty;

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Module, ModuleGridViewModel>()
                .ForMember(m => m.Author, opt => opt.MapFrom(e => e.Author.UserName))
                .ForMember(m => m.AuthorEmail, opt => opt.MapFrom(e => e.Author.Email))
                .ForMember(m => m.ImageData, opt => opt.MapFrom(e => e.Image.ImageData));
        }
    }
}