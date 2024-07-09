using LearningPlatform.Application.Features.Lesson.Commands.Create;
using LearningPlatform.Application.Features.Lesson.Queries.GetAllLessonsbyCourseId;
using LearningPlatform.Application.Features.Lesson.Queries.GetByLessonId;
using LearningPlatform.Application.Features.Student.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.API.Controllers
{
    public class LessonController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateLessonCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getbycourse/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIdStudentByEmail(Guid id)
        {
            var query = new GetAllLessonsbyCourseIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetByLessonIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

    }
}
