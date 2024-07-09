using LearningPlatform.Application.Features.Course.Commands.Create;
using LearningPlatform.Application.Features.Course.Commands.Update;
using LearningPlatform.Application.Features.Course.Queries.GetAll;
using LearningPlatform.Application.Features.Course.Queries.GetById;
using LearningPlatform.Application.Features.Course.Queries.GetByIdTeacher;
using LearningPlatform.Application.Features.Student.Commands.Create;
using LearningPlatform.Application.Features.Student.Commands.Update;
using LearningPlatform.Application.Features.Student.Queries.GetAll;
using LearningPlatform.Application.Features.Student.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.API.Controllers
{
    public class CourseController : ApiControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateCourseCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        

        [HttpPatch("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateCourseCommand command, Guid id)
        {
            if (command.CourseId != id)
            {
                return BadRequest("Id mismatch.");
            }

     

            var result = await Mediator.Send(command);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllCoursesQuery());
            return Ok(result);
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetByIdCourseQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }




        [HttpGet("teacher/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTeacherId(Guid id)
        {
            var query = new GetCoursesIdsbyTeacherIdQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }



    }
}
