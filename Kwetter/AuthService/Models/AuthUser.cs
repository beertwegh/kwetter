﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class AuthUser
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
