namespace HappyMe.Data.Contracts.Models
{
    using System;

    public interface IAuditInfo : IEntity
    {
        DateTime CreatedOn { get; set; }

        bool PreserveCreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
