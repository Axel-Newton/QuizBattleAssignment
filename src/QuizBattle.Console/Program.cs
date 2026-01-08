using QuizBattle.Console;

QuizUtils.SeedQuestions();

Console.WriteLine("Welcome to Quiz Battle!");

Console.WriteLine("Quiz Battle regler:");
Console.WriteLine("Svara på frågor korrekt för att tjäna poäng.");

while (!QuizUtils.IsCompleted())
{
    QuizUtils.DisplayQuestion();
    int answer = QuizUtils.GetAnswer();

    QuizUtils.CheckAnswer(answer);

    QuizUtils.WriteStatus();
}

Console.WriteLine("Tack för att du spelade Quiz Battle!");
