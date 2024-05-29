using LLAmaSharp.Shared.Models;

//INIT KOUREAS STAVROS
namespace LLAmaSharp.Server.Interfaces
{
    public interface Interface_Chat
    {
        public IAsyncEnumerable<string> DoCommunicateAsync(ChatRequest chat);
    }
}
