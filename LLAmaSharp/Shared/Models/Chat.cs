namespace LLAmaSharp.Shared.Models
{
    public class ChatRequest
    {
        public string context { get; set; } = null!;
        public string prompt { get; set; } = null!;
    }

    public class ChatResponse
    {
        public string? response { get; set; }
    }
}
