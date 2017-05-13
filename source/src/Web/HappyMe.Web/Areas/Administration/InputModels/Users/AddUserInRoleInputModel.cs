namespace HappyMe.Web.Areas.Administration.InputModels.Users
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;

    public class AddUserInRoleInputModel : IMapFrom<User>, IMapTo<IdentityUserRole<string>>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [ReadOnly(true)]
        [DisplayName("Потребител")]
        ////[Enabled(false)]
        ////[PlaceHolder("Потребител")]
        public string Username { get; set; }

        [DisplayName("Роля")]
        [UIHint("StringDropDownList")]
        public string RoleId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, AddUserInRoleInputModel>()
                .ForMember(m => m.UserId, opt => opt.MapFrom(e => e.Id));
        }
    }
}
