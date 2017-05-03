namespace HappyMe.Web.Areas.Administration.InputModels.UsersInModules
{
    using System.ComponentModel.DataAnnotations;

    using HappyMe.Common.Mapping;
    using HappyMe.Data.Models;

    public class UserInModuleCreateInputModel : IMapFrom<UserInModule>, IMapTo<UserInModule>
    {
        // TODO: Get users with autocomplete
        [UIHint("StringDropDownList")]
        public string UserId { get; set; }
        
        [UIHint("DropDownList")]
        public int ModuleId { get; set; }
    }
}