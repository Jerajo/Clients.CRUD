using System;

namespace Clients.Core.Contracts
{
    /// <summary>
    /// Represents a entity with the minimum information for the database.
    /// </summary>
    /// <typeparam name="T">The entities id type.</typeparam>
    public interface Entity<T>
    {
        /// <summary>
        /// The entity unique identifier.
        /// </summary>
        T Id { get; set; }

        /// <summary>
        /// The unique identifier for the entity creator.
        /// </summary>
        T CreatedBy { get; set; }

        /// <summary>
        /// The date and time where the entity was created.
        /// </summary>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// The unique identifier for the user who update the entity.
        /// </summary>
        T UpdatedBy { get; set; }

        /// <summary>
        /// The date and time where the entity was updated.
        /// </summary>
        DateTime UpdatedDate { get; set; }
    }
}
