using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ChatGPTService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public ChatGPTService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = "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";  
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<string> GetChatGPTResponse(string prompt)
    {
        var request = new
        {
            model = "gpt-3.5-turbo", 
            messages = new[] { new { role = "user", content = prompt } },
            max_tokens = 150
        };

        var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", request);
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadFromJsonAsync<ChatGPTResponse>();

        return responseData?.Choices?[0]?.Message?.Content?.Trim() ?? "No response";
    }
}

public class ChatGPTResponse
{
    public List<Choice> Choices { get; set; }

    public class Choice
    {
        public Message Message { get; set; }
    }

    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }
}
