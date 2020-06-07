using System;
using MessageService.Helpers;
using MessageService.Models;
using MessageService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("new")]
        [GetCurrentUser]
        public IActionResult NewMessage([FromBody] Message message)
        {
            if (GetCurrentUser.UserId == Guid.Empty)
                return Unauthorized();
            _messageService.NewMessage(message, GetCurrentUser.UserId);
            return Ok();
        }

        [HttpGet("all")]
        [GetCurrentUser]
        public IActionResult GetAllMessages()
        {
            if (GetCurrentUser.UserId == Guid.Empty)
                return Unauthorized();
            var messages = _messageService.GetAllMessages();
            return Ok(messages);
        }
    }
}