using System;

namespace Clients.Application.DTOs
{
    /// <summary>
    /// Represents a costumer's address for the business.
    /// </summary>
    public class AddressDto : BaseDto<Guid>
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

        /// <summary>
        /// The FK to reference the client's address.
        /// </summary>
        public Guid ClientId { get; set; }
    }
}
