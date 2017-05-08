namespace HappyMe.Web.Areas.Administration.ViewModels.Dashboard
{
    using System;

    using AutoMapper;

    using HappyMe.Common.Constants;
    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class ChildViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public byte[] ProfileImage { get; set; }

        public string ImageUrl => this.ProfileImage != null ? string.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String(this.ProfileImage)) : GlobalConstants.DefaultUserImagePath;

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            ////configuration.CreateMap<User, ChildViewModel>()
            ////    .ForMember(m => m.ProfileImage, opt => opt.MapFrom(e => (e.ProfileImage != null ? e.ProfileImage.ImageData : null));
        }
    }
}