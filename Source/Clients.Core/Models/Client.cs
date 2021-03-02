using System;

namespace Clients.Core.Models
{
    /// <summary>
    /// Represents a costumer for the business.
    /// </summary>
    public class Client : BaseEntity<Guid>
    {
        /// <summary>
        /// Holds the client's full name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The unique user name used for login and public display.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Holds the client's electronic mail.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Holds the client's birth day. [Min: 1/1/0001 12:00:00 AM | Max: 23:59:59.9999999 UTC, December 31, 9999]
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// Holds the client's sex. [F: for female | M: for male].
        /// </summary>
        public char Sex { get; set; }

    }
}
