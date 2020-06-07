using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models
{
    public class UserRegistrationModel
    {
        [Key] public Guid UserId { get; set; }
        public string ProfileName { get; set; }

    }
}
