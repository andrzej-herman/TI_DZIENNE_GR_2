using Quiz.Data;
using System.Text.Json;

namespace Quiz.Application
{
    public class GameService : IGameService
    {
        private readonly HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public GameService(HttpClient client)
        {
            _client = client;
            _serializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        }

        public async Task<CheckAnswerDto?> CheckAnswer(string answerId, int category)
        {
            var url = $"checkanswer?answerId={answerId}&category={category}";
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                var check = JsonSerializer.Deserialize<CheckAnswerDto>(text, _serializerOptions);
                return check;
            }
            else
                return null;
        }

        public async Task<QuestionDto?> GetQuestion(int category)
        {
            var url = $"getquestion?category={category}";
            var response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                var question = JsonSerializer.Deserialize<QuestionDto>(text, _serializerOptions);
                return question;
            }
            else
                return null;
        }
    }
}
