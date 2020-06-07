using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AuthService.Models
{
    public class AuthUser
    {
        [Key] public int AuthUserId { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public bool IsValid => !(string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password));
    }
}
