
using System;
using QuizBattle.Console;
using Xunit;

namespace QuizBattle.Tests
{
    public class QuestionServiceTests
    {
        [Fact]
        public void GetRandomQuestions_CountGreaterOrEqualToTotal_Throws()
        {
            var repo = new InMemoryQuestionRepository();
            var service = new QuestionService(repo);

            // För närvarande finns 3 seedade frågor. Begär 4 => ska kasta.
            Assert.Throws<ArgumentOutOfRangeException>(() => service.GetRandomQuestions(4));
        }
    }
}
