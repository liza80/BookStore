using System.ComponentModel.DataAnnotations;

namespace WebApplication15.Models
{
    public class User
    {
        public long Id { get; set; }

        public required string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }

        public ICollection<Order>? Orders { get; set; }

    }
}
