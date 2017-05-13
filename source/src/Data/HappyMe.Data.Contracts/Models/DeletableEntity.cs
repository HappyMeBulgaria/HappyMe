namespace HappyMe.Data.Contracts.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class DeletableEntity : AuditInfo, IDeletableEntity
    {
        // [Index] TODO: Move index to modelbuilder
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
