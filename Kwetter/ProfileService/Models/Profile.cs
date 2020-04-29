using System;
using System.ComponentModel.DataAnnotations;

namespace ProfileService.Models
{
    public class Profile
    {
        [Key] public int Id { get; set; }
        public Guid UserId { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
        
    }
}
