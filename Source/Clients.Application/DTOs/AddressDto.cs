using System;

namespace Clients.Application.DTOs
{
    /// <summary>
    /// Represents a client's address entity with all its public attributes.
    /// </summary>
    public class AddressDto : AddressForCreationDto
    {
        /// <summary>
        /// The entity unique identifier.
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents a client's address entity with only the attributes needed to create one.
    /// </summary>
    public class AddressForCreationDto : AddressForEditionDto
    {
        /// <summary>
        /// The FK to reference the client's address.
        /// </summary>
        public Guid ClientId { get; set; }
    }

    /// <summary>
    /// Represents a client's address entity with only the attributes needed to update one.
    /// </summary>
    public class AddressForEditionDto
    {
        /// <summary>
        /// The current client's country name.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The current client's state name.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The current client's city name.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The current client's 5 digit postal code name.
        /// </summary>
        public uint PostalCode { get; set; }

        /// <summary>
        /// The client's address description 1rt line.
        /// </summary>
        public string AddressLineOne { get; set; }

        /// <summary>
        /// The client's address description 2nd line.
        /// </summary>
        public string AddressLineTwo { get; set; }
    }
}
