using System;

namespace PrjModule24.Models
{
    public record UserProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}