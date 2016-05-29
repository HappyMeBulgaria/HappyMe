using Microsoft.AspNet.Identity.EntityFramework;

namespace HappyMe.Web.Areas.Administration.InputModels.Users
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;
    using HappyMe.Web.Common.Attributes;

    public class AddUserInRoleInputModel : IMapFrom<User>, IMapTo<IdentityUserRole>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [ReadOnly(true)]
        [DisplayName("Потребител")]
        [Enabled(false)]
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