using LLAmaSharp.Server.Interfaces;
using LLAmaSharp.Server.Managers.Communication;
using LLAmaSharp.Shared.Models;

//INIT KOUREAS STAVROS
namespace LLAmaSharp.Server.Services
{
    public class Service_Chat : Interface_Chat
    {
        readonly IHttpContextAccessor _httpContextAccessor;

        public Service_Chat(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IAsyncEnumerable<string> DoCommunicateAsync(ChatRequest chat)
        {
            return Chatengine.ChatLocalAsync(chat);
        }
    }
}