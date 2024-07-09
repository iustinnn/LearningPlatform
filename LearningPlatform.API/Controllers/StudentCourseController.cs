using LearningPlatform.Application.Features.Course.Commands.Create;
using LearningPlatform.Application.Features.Course.Queries.GetById;
using LearningPlatform.Application.Features.StudentCourse.Commands.Create;
using LearningPlatform.Application.Features.StudentCourse.Commands.Delete;
using LearningPlatform.Application.Features.StudentCourse.Commands.Update;
using LearningPlatform.Application.Features.StudentCourse.Queries.GetAllCoursesByStudentId;
using LearningPlatform.Application.Features.StudentCourse.Queries.GetAllStudentsByCourseId;
using LearningPlatform.Application.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.API.Controllers
{
    public class StudentCourseController : ApiControllerBase
    {
        private readonly IStudentCourseRepository _studentCourseRepository;
        
        public StudentCourseController(IStudentCourseRepository studentCourseRepository)
        {
            _studentCourseRepository = studentCourseRepository;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateStudentCourseCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("studentid/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetAllCoursesByStudentIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("courseid/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCourseId(Guid id)
        {
            var query = new GetAllStudentsByCourseIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(DeleteStudentCourseCommand command)
        {
           

            var result = await Mediator.Send(command);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }
        [HttpPatch("update")]
        public async Task<IActionResult> Update(UpdateStudentCourseCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid payload.");
            }

            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


    }
}
