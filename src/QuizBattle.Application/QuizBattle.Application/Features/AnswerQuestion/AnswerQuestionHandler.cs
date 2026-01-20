using System;
using System.Threading.Tasks;
using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.AnswerQuestion
{
    public class AnswerQuestionHandler
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IQuestionRepository _questionRepository;

        public AnswerQuestionHandler(ISessionRepository sessionRepository, IQuestionRepository questionRepository)
        {
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        }

        public async Task<AnswerQuestionResult> HandleAsync(AnswerQuestionCommand command)
        {
             if (command == null) throw new ArgumentNullException(nameof(command));
             
             // Validate that session exists
             var session = await _sessionRepository.GetByIdAsync(command.SessionId);
             if (session == null) throw new InvalidOperationException("Session not found");
             
             // Validation that question exists
             var question = await _questionRepository.GetByCodeAsync(command.QuestionCode);
             if (question == null) throw new InvalidOperationException("Question not found");

             bool isCorrect = question.IsCorrect(command.SelectedChoiceCode);
             
             session.SubmitAnswer(question, command.SelectedChoiceCode, DateTime.UtcNow);
             
             await _sessionRepository.UpdateAsync(session);
             
             return new AnswerQuestionResult { IsCorrectAnswer =  isCorrect };

        }
    }
}