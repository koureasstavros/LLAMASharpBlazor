using LLama;
using LLama.Common;
using LLAmaSharp.Shared.Models;

namespace LLAmaSharp.Server.Managers.Communication
{
    public static class Chatengine
    {
        private static LLamaWeights? model;
        private static LLamaContext? context;

        public async static IAsyncEnumerable<string> ChatLocalAsync(ChatRequest chat)
        {
            //CONFIGS
            //MODEL FILE SIZE DOES NOT RELATE WITH MEMORY UTILIAZATION
            //"phi3mini_fp16.gguf"; //LOADING 1.5GB IN MEMORY
            //"phi3mini_q4.gguf"; //LOADING 1.5GB IN MEMORY
            string param_model_path = "models/phi3mini_q4.gguf"; //USED FOR CUSTOM MODEL PATH

            CancellationToken cancellationToken = new CancellationToken();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            var parameters = new ModelParams(param_model_path)
            {
                ContextSize = 4096, // The longest length of chat as memory.
                GpuLayerCount = 0 // How many layers to offload to GPU. Please adjust it according to your GPU memory.
            };

            if (model == null)
            { model = LLamaWeights.LoadFromFile(parameters); }
            if (context == null)
            { context = model.CreateContext(parameters); }

            var executor = new InteractiveExecutor(context);

            // Add chat histories as prompt to tell AI how to act.
            var chatHistory = new ChatHistory();
            chatHistory.AddMessage(AuthorRole.System, chat.context);
            chatHistory.AddMessage(AuthorRole.User, "Hello, I need your support.");
            chatHistory.AddMessage(AuthorRole.Assistant, "Hello, How may I help you today?");

            InferenceParams inferenceParams = new InferenceParams()
            {
                MaxTokens = 1024, // No more than 256 tokens should appear in answer. Remove it if antiprompt is enough for control.
                AntiPrompts = new List<string> { "User:" } // Stop generation once antiprompts appear.
            };

            ChatSession session = new(executor, chatHistory);
            Console.Write("The chat session has started.");

            await foreach (string? result in session.ChatAsync(new ChatHistory.Message(AuthorRole.User, chat.prompt), inferenceParams, cancellationToken))
            {
                if (result != null)
                { yield return result; }
            }
        }
    }
}
