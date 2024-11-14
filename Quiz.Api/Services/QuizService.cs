using Microsoft.Data.SqlClient;
using Quiz.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace Quiz.Api.Services
{
    public class QuizService : IQuizService
    {

        // NAZWA SERWERA LUB ADRES IP BAZY DANYCH
        // NAZWA BAZY DANYCH
        // UZYTKOWNIK
        // HASŁO

        // CONNECTION STRING
        // ""Server=.\\HERMANLOCAL;Database=CqrsTp1;Integrated Security=True;TrustServerCertificate=True;"

        private readonly SqlConnection _sqlConnection;
        private readonly Random _random;

        public QuizService()
        {
            var connString = "Server=.\\HERMANLOCAL;Database=CqrsTp1;Integrated Security=True;TrustServerCertificate=True";
            _sqlConnection = new SqlConnection(connString);
            _random = new Random();
        }


        public QuestionDto GetQuestion(int category)
        {
            var questions = new List<QuestionDto>();
            _sqlConnection.Open();
            var query = $"SELECT * FROM Questions WHERE QuestionCategory = {category}";
            var command  = new SqlCommand(query, _sqlConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetGuid(0);
                var qCat = reader.GetInt32(1);
                var qCont = reader.GetString(2);
                var q = new QuestionDto
                {
                    Id = id,
                    Category = qCat,
                    Content = qCont,
                    Answers = new List<AnswerDto>()
                };

                questions.Add(q);
            }
            reader.Close();

            var number = _random.Next(0, questions.Count);
            var question = questions[number];

            var queryAnswers = $"SELECT * FROM Answers WHERE QuestionId = '{question.Id}'";
            var commandAnswers = new SqlCommand(queryAnswers, _sqlConnection);
            var readerAnswers = commandAnswers.ExecuteReader();
            while (readerAnswers.Read())
            {
                var aId = readerAnswers.GetGuid(0);
                var aCont = readerAnswers.GetString(1);
                var a = new AnswerDto
                {
                    Id = aId,
                    Content = aCont,
                };

                question!.Answers!.Add(a);
            }
            readerAnswers.Close();
            _sqlConnection.Close();
            return question;
        }



        public CheckAnswerDto CheckAnswer(Guid answerId, int category)
        {
            List<int> allCategories = [100, 200, 300, 400, 500, 750, 1000];
            var nextCategory = 0;
            bool isCorrect = false;
            _sqlConnection.Open();
            var query = $"SELECT AnswerIsCorrect FROM Answers WHERE AnswerId = '{answerId}'";
            var command = new SqlCommand(query, _sqlConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                isCorrect = reader.GetBoolean(0);
            }
            reader.Close();
            _sqlConnection.Close();
            var index = allCategories.IndexOf(category);

            if (index != 6)
                nextCategory = allCategories[index + 1];


            return new CheckAnswerDto { IsCorrectAnswer = isCorrect, NextCategory = nextCategory };
        }

       
    }
}
