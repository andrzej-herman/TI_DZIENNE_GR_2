using Quiz.Data;

namespace Quiz.Api.Services
{
    public interface IQuizService
    {
        bool CheckAnswer(int answerId);
        QuestionDto GetQuestion(int category);
    }
}
