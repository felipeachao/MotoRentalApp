using System;

namespace MotoRental.Domain.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public required string Event { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
