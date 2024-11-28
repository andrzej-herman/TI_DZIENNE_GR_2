using Quiz.Data;

namespace Quiz.Application
{
    public interface IGameService
    {
        Task<QuestionDto?> GetQuestion(int category);
        Task<CheckAnswerDto?> CheckAnswer(string answerId, int category);
    }
}
