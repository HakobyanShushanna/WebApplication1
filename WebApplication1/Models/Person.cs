using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class Person:IdentityUser
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
