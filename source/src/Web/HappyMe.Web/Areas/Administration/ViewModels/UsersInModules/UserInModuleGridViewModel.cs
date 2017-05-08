namespace HappyMe.Web.Areas.Administration.ViewModels.UsersInModules
{
    using System;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class UserInModuleGridViewModel : IMapFrom<UserInModule>, IHaveCustomMappings
    {
        public string UserName { get; set; }
        
        public string ModuleName { get; set; }

        public string UserId { get; set; }

        public int ModuleId { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<UserInModule, UserInModuleGridViewModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(e => e.User.UserName))
                .ForMember(m => m.ModuleName, opt => opt.MapFrom(e => e.Module.Name));
        }
    }
}