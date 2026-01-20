using System;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public class AnswerQuestionCommand
    {
        public Guid SessionId { get; }
        public string QuestionCode { get; }
        public string SelectedChoiceCode { get; }

        public AnswerQuestionCommand(Guid sessionId, string questionCode, string selectedChoiceCode)
        {
            SessionId = sessionId;
            QuestionCode = questionCode ?? throw new ArgumentNullException(nameof(questionCode));
            SelectedChoiceCode = selectedChoiceCode ?? throw new ArgumentNullException(nameof(selectedChoiceCode));
        }
    }
}