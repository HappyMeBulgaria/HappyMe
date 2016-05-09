// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuditInfo.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Te4Fest.Data.Contracts.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class AuditInfo : IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Specifies whether or not the CreatedOn property should be automatically set.
        /// </summary>
        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
