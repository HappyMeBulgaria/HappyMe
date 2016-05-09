// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuditInfo.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Te4Fest.Data.Contracts.Models
{
    using System;

    public interface IAuditInfo : IEntity
    {
        DateTime CreatedOn { get; set; }

        bool PreserveCreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
