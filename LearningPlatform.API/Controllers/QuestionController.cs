using LearningPlatform.Application.Contracts;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningPlatform.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet("quiz/{quizId}")]
        public async Task<IActionResult> GetQuestionsByQuizId(Guid quizId)
        {
            var result = await _questionRepository.GetQuestionsByQuizId(quizId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpGet("{questionId}")]
        public async Task<IActionResult> GetQuestionById(Guid questionId)
        {
            var result = await _questionRepository.GetQuestionById(questionId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
        {
            var question = Question.Create(request.Text, request.Order, request.Answer1, request.Answer2, request.Answer3, request.CorrectAnswer, request.QuizId);
        

            var result = await _questionRepository.CreateQuestionAsync(question.Value);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetQuestionById), new { questionId = result.Value }, result.Value);
            }
            return BadRequest(result.Error);
        }

        public class CreateQuestionRequest
        {
            public string Text { get; set; }
            public int Order { get; set; }
            public string Answer1 { get; set; }
            public string Answer2 { get; set; }
            public string Answer3 { get; set; }
            public string CorrectAnswer { get; set; }
            public Guid QuizId { get; set; }
        }
    }
}
