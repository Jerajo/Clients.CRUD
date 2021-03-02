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
        /// The unique identifier for the identity creator.
        /// </summary>
        T CreatedBy { get; set; }

        /// <summary>
        /// The unique identifier for the identity creator.
        /// </summary>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// The unique identifier for the user who update identity.
        /// </summary>
        T UpdatedBy { get; set; }

        /// <summary>
        /// The unique identifier for the identity creator.
        /// </summary>
        DateTime UpdatedDate { get; set; }
    }
}
