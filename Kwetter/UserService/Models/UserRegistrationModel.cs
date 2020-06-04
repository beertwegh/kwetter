using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessageService.Models
{
    public class UserRegistrationModel : User
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string ProfileName { get; set; }


        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public UserRegistrationModel()
        {
            
        }
        public UserRegistrationModel(string email, string location, string web, string bio, Guid userGuid) : base(email, location, web, bio, userGuid)
        {
        }
    }
}
