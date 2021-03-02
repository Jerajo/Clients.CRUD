namespace Clients.Application.DTOs
{
    /// <summary>
    /// Represents a model with the minimum information.
    /// </summary>
    /// <typeparam name="T">The entities id type.</typeparam>
    public class BaseDto<T>
    {
        /// <summary>
        /// The entity unique identifier.
        /// </summary>
        T Id { get; set; }
    }
}
