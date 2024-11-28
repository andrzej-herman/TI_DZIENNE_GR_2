using Quiz.Data;

namespace Quiz.Api.Services
{
    public interface IQuizService
    {
        Task<CheckAnswerDto> CheckAnswer(string answerId, int category);
        Task<QuestionDto> GetQuestion(int category);
    }
}
