using System;

namespace Clients.Core.Entities
{
    public abstract class BaseEntity<T> : Contracts.Entity<T>
    {
        /// <inheritdoc/>
        public T Id { get; set; }

        /// <inheritdoc/>
        public T CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedDate { get; set; }

        /// <inheritdoc/>
        public T UpdatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime UpdatedDate { get; set; }
    }
}
