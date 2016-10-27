namespace HappyMe.Web.Models.Areas.Administration.InputModels.UsersInModules
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;

    using HappyMe.Common.Mapping;

    public class UserInModuleCreateInputModel : IMapFrom<UserInModule>, IMapTo<UserInModule>
    {
        // TODO: Get users with autocomplete
        [UIHint("StringDropDownList")]
        public string UserId { get; set; }
        
        [UIHint("DropDownList")]
        public int ModuleId { get; set; }
    }
}