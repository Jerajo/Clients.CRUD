namespace Clients.Core.Models
{
    public abstract class BaseEntity<T> : Contracts.Entity<T>
    {
        public T Id { get; set; }
    }
}
