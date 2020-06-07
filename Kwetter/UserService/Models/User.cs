using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class User 
    {
        [Key]public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Web { get; set; }
        public string Bio { get; set; }

        public User()
        {
            
        }
        public User(string email, string location, string web, string bio, Guid userId)
        {
            UserId = userId;
            Email = email;
            Location = location;
            Web = web;
            Bio = bio;
        }
    }
}
