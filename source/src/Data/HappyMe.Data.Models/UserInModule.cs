namespace HappyMe.Data.Models
{
    using HappyMe.Data.Contracts.Models;

    public class UserInModule : DeletableEntity
    {
        public string UserId { get; set; }
        
        public int ModuleId { get; set; }

        public virtual User User { get; set; }

        public virtual Module Module { get; set; }
    }
}
