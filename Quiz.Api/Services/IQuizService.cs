using Quiz.Data;

namespace Quiz.Api.Services
{
    public interface IQuizService
    {
        CheckAnswerDto CheckAnswer(Guid answerId, int category);
        QuestionDto GetQuestion(int category);
    }
}
