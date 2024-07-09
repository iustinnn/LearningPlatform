using LearningPlatform.App.Contracts;
using LearningPlatform.Application.Features.Lesson.Commands.Create;
using LearningPlatform.Application.Features.Lesson.Queries.GetAllLessonsbyCourseId;
using LearningPlatform.Application.Features.Lesson.Queries.GetByLessonId;
using LearningPlatform.Application.Persistence;
using LearningPlatform.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.API.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ApiControllerBase
    {
        private readonly IQuizRepository _quizRepository;

        public QuizController(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetQuizzesByCourseId(Guid courseId)
        {
            var result = await _quizRepository.GetQuizzesByCourseId(courseId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpGet("{quizId}")]
        public async Task<IActionResult> GetQuizById(Guid quizId)
        {
            var result = await _quizRepository.GetQuizById(quizId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizRequest request)
        {
            var quiz = Domain.Entities.Quiz.Create(request.Title, request.CourseId, request.Description);    
 
            var result = await _quizRepository.CreateQuizAsync(quiz.Value);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetQuizById), new { quizId = result.Value.Id }, result.Value);
            }
            return BadRequest(result.Error);
        }

        public class CreateQuizRequest
        {
            public string Title { get; set; }

            public string Description { get; set; }
            public Guid CourseId { get; set; }
        }

    }
}
