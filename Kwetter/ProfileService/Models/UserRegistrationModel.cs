using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Models
{
    public class UserRegistrationModel
    {
        [Key] public Guid UserId { get; set; }
        public string ProfileName { get; set; }

    }
}
