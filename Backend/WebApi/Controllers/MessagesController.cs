using Application.Messages.GetGroupMessage;
using Application.Messages.GetUserMessage;
using Application.Messages.SendGroupMessage;
using Application.Messages.SendUserMessage;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : ApiController
    {
        private readonly ISender _sender;

        public MessagesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserMessages(string receiverId,string senderId)
        {
            var res = await _sender.Send(new GetUserMessageQuery(receiverId,senderId));
            return res.IsSuccess ? Ok(res.Value) : Problem(res.Errors);
        }

        [HttpGet("group")]
        public async Task<IActionResult> GetGroupMessages(string receiverId, string senderId)
        {
            var res = await _sender.Send(new GetGroupMessageQuery(receiverId,senderId));
            return res.IsSuccess ? Ok(res.Value) : Problem(res.Errors);
        }


        [HttpPost("user")]
        public async Task<IActionResult> SendUserMessages(SendUserMessageCommand command)
        {
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok() : Problem(res.Errors);
        }

        [HttpPost("group")]
        public async Task<IActionResult> SendGroupMessages(SendGroupMessageCommand command)
        {
            var res = await _sender.Send(command);
            return res.IsSuccess ? Ok() : Problem(res.Errors);
        }
    }
}
