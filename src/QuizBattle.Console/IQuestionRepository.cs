using QuizBattle.Domain;

namespace QuizBattle.Console
{
    public interface IQuestionRepository
    {
        List<Question> GetAll();
    }
}
