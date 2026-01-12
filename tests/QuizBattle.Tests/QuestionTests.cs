using QuizBattle.Domain;
using Xunit;

namespace QuizBattle.Tests
{
    public class QuestionTests
    {
        [Fact]
        public void Constructor_NullParameters_ThrowsDomainException()
        {
            Assert.Throws<DomainException>(() => new Question(null!, null!, null!, null!));
        }

        [Fact]
        public void Constructor_EmptyChoices_ThrowsDomainException()
        {
            Assert.Throws<DomainException>(() =>
                new Question("Q1", "Question", new Choice[] { }, ""));
        }
    }
}
