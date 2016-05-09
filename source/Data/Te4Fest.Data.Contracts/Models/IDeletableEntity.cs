// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeletableEntity.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Te4Fest.Data.Contracts.Models
{
    using System;

    public interface IDeletableEntity : IEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
