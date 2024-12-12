using Microsoft.Data.SqlClient;
using Quiz.Data;

namespace Quiz.Api.Services
{
    public class QuizService : IQuizService
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Random _random;

        public QuizService()
        {
            var connString = "Server=tcp:projektysan.database.windows.net,1433;Initial Catalog=sanquiz;Persist Security Info=False;User ID=aherman;Password=yxFH#D8w1SabJ1TAH99f;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            _sqlConnection = new SqlConnection(connString);
            _random = new Random();
        }


        public async Task<QuestionDto> GetQuestion(int category)
        {
            var questions = new List<QuestionDto>();
            await _sqlConnection.OpenAsync();
            var query = $"SELECT * FROM Questions WHERE QuestionCategory = {category}";
            var command  = new SqlCommand(query, _sqlConnection);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var id = reader.GetGuid(0);
                var qCat = reader.GetInt32(1);
                var qCont = reader.GetString(2);
                var q = new QuestionDto
                {
                    Id = id.ToString(),
                    Category = qCat,
                    Content = qCont,
                    Answers = new List<AnswerDto>()
                };

                questions.Add(q);
            }
            await reader.CloseAsync();

            var number = _random.Next(0, questions.Count);
            var question = questions[number];

            var queryAnswers = $"SELECT * FROM Answers WHERE QuestionId = '{question.Id}'";
            var commandAnswers = new SqlCommand(queryAnswers, _sqlConnection);
            var readerAnswers = await commandAnswers.ExecuteReaderAsync();
            while (await readerAnswers.ReadAsync())
            {
                var aId = readerAnswers.GetGuid(0);
                var aCont = readerAnswers.GetString(1);
                var a = new AnswerDto
                {
                    Id = aId.ToString(),
                    Content = aCont,
                };

                question!.Answers!.Add(a);
            }
            await readerAnswers.CloseAsync();
            await  _sqlConnection.CloseAsync();
            return question;
        }



        public async Task<CheckAnswerDto> CheckAnswer(string answerId, int category)
        {
            List<int> allCategories = [100, 200, 300, 400, 500, 750, 1000];
            var nextCategory = 0;
            bool isCorrect = false;
            await _sqlConnection.OpenAsync();
            var query = $"SELECT AnswerIsCorrect FROM Answers WHERE AnswerId = '{answerId}'";
            var command = new SqlCommand(query, _sqlConnection);
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                isCorrect = reader.GetBoolean(0);
            }
            await reader.CloseAsync();
            await _sqlConnection.CloseAsync();
            var index = allCategories.IndexOf(category);

            if (index != 6)
                nextCategory = allCategories[index + 1];


            return new CheckAnswerDto { IsCorrectAnswer = isCorrect, NextCategory = nextCategory };
        }

       
    }
}
