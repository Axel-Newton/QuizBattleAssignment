using QuizBattle.Console;

var questions = QuestionUtils.SeedQuestions();

Console.Title = "QuizBattle – Konsol (v.2 dag 1–2)";
Console.WriteLine("Välkommen till QuizBattle!");
Console.WriteLine("Detta är en minimal code‑along‑loop för dag 1–2 (3 frågor).");
Console.WriteLine("Tryck valfri tangent för att starta...");

Console.ReadKey(true);
Console.WriteLine();

while (!QuestionUtils.IsCompleted())
{
    QuestionUtils.DisplayQuestion();
    int answer = QuestionUtils.GetAnswer();

    QuestionUtils.CheckAnswer(answer);

    QuestionUtils.WriteStatus();
}

Console.WriteLine("Tack för att du spelade Quiz Battle!");
