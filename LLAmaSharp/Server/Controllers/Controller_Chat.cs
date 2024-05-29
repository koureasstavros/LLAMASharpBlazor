using Microsoft.AspNetCore.Mvc;
using LLAmaSharp.Server.Interfaces;
using LLAmaSharp.Shared.Models;

//INIT KOUREAS STAVROS
namespace LLAmaSharp.Server.Controllers
{
    [Route("/api/chat")]
    [ApiController]
    public class Controller_Chat : ControllerBase
    {
        private readonly Interface_Chat _IChat;

        public Controller_Chat(Interface_Chat iChat)
        {
            _IChat = iChat;
        }

        [HttpPost, Route("/api/chat/communicate_async")]
        public async IAsyncEnumerable<string> DoCommunicateAsync(ChatRequest chat)
        {
            //ADD RESPONSE HEADER
            //Response.Headers.Append("Content-Type", "application/octet-stream"); //ENABLES STREAMING ON CLIENT
            //Response.Headers.Append("Content-Type", "application/stream+json"); //ENABLES STREAMING ON CLIENT
            //Response.Headers.Append("Content-Type", "multipart/form-data");
            //Response.Headers.Append("Content-Type", "text/event-stream");
            //Response.Headers.Append("Content-Type", "application/json");
            //Response.Headers.Append("Content-Type", "text/event-stream");
            //Response.Headers.Append("Cache-Control", "no-cache");
            //Response.Headers.Append("Connection", "keep-alive");

            IAsyncEnumerable<string> results = _IChat.DoCommunicateAsync(chat);
            await foreach (var result in results)
            {
                yield return result;
            }

            //NdjsonAsyncEnumerableResult<string> results = new NdjsonAsyncEnumerableResult<string>(_IChat.DoCommunicateAsync(chat));
            //return results;
        }
    }
}
