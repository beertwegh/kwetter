using Newtonsoft.Json;
using System;

namespace ProfileService.Models
{
    public class EditProfileName
    {
        public Guid UserId { get; set; }
        public string NewName { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
