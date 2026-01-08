namespace QuizBattle.Console;

public  class Question
{
    public Question(Choice[] choices)
    {
        Choices = choices.ToList();
    }

    public List<Choice> Choices { get; }
    public Choice CorrectAnswer { get; }
}