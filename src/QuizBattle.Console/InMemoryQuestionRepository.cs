using QuizBattle.Domain;

namespace QuizBattle.Console
{
    public class InMemoryQuestionRepository : IQuestionRepository
    {
        public List<Question> GetAll() => QuestionUtils.SeedQuestions();

        private static class QuestionUtils
        {
            public static List<Question> SeedQuestions()
            {
                return new List<Question>
                {
                    new Question(
                        "Q.CS.001",
                        "Vad gör 'using'-statement i C#?",
                        new[]
                        {
                            new Choice("Q.CS.001.A","Skapar en ny tråd"),
                            new Choice("Q.CS.001.B","Säkerställer korrekt Dispose av resurser"),
                            new Choice("Q.CS.001.C","Importerar ett NuGet-paket")
                        },
                        "Q.CS.001.B"
                    ),
                    new Question(
                        "Q.CS.002",
                        "Vad innebär 'var' i C#?",
                        new[]
                        {
                            new Choice("Q.CS.002.A","Dynamisk typ vid runtime"),
                            new Choice("Q.CS.002.B","Implicit, men statisk, typinferens vid compile-time"),
                            new Choice("Q.CS.002.C","Alias för object")
                        },
                        "Q.CS.002.B"
                    ),
                    new Question(
                        "Q.OOP.011",
                        "Vad beskriver inkapsling bäst?",
                        new[]
                        {
                            new Choice("Q.OOP.011.A","Göm implementation, exponera kontrollerat gränssnitt"),
                            new Choice("Q.OOP.011.B","Ärva från flera basklasser"),
                            new Choice("Q.OOP.011.C","Skapa statiska metoder")
                        },
                        "Q.OOP.011.A"
                    )
                };
            }
        }
    }
}
