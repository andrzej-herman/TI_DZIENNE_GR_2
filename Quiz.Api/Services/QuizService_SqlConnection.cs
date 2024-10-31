using Quiz.Data;

namespace Quiz.Api.Services
{
    public class QuizService_SqlConnection : IQuizService
    {
        public bool CheckAnswer(int answerId)
        {
            // SELECT IsCorrect FROM Answers WHERE AnswerId = answerId
            throw new NotImplementedException();
        }

        public QuestionDto GetQuestion(int category)
        {
            //   SELECT * FROM Questions WHERE QuestionCategory = 100
            throw new NotImplementedException();
        }
    }
}
