namespace HappyMe.Web.Models.Areas.Administration.InputModels.Users
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using Common.Attributes;

    using Data.Models;

    using HappyMe.Common.Mapping;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class AddUserInRoleInputModel : IMapFrom<User>, IMapTo<IdentityUserRole>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [ReadOnly(true)]
        [DisplayName("Потребител")]
        [Enabled(false)]
        [PlaceHolder("Потребител")]
        public string Username { get; set; }

        [DisplayName("Роля")]
        [UIHint("StringDropDownList")]
        public string RoleId { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, AddUserInRoleInputModel>()
                .ForMember(m => m.UserId, opt => opt.MapFrom(e => e.Id));
        }
    }
}