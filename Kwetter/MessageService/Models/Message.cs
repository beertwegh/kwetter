using System;
using System.ComponentModel.DataAnnotations;

namespace MessageService.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public string MessageText { get; set; }

    }
}
