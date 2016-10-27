namespace HappyMe.Web.Models.Areas.Administration.ViewModels.Modules
{
    using System;

    using AutoMapper;

    using Data.Models;

    using HappyMe.Common.Mapping;

    public class ModuleGridViewModel : IMapFrom<Module>, IMapTo<Module>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string UserName { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageUrl => this.ImageData != null ?
            $"data:image/jpeg;base64,{Convert.ToBase64String(this.ImageData)}"
            : string.Empty;

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Module, ModuleGridViewModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(e => e.Author.UserName))
                .ForMember(m => m.ImageData, opt => opt.MapFrom(e => e.Image.ImageData));
        }
    }
}