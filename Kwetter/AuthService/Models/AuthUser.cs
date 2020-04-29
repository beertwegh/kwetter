using System;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class AuthUser
    {

        public Guid UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
