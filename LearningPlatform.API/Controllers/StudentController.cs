using LearningPlatform.Application.Features.Course.Commands.Update;
using LearningPlatform.Application.Features.Course.Queries.GetByIdTeacher;
using LearningPlatform.Application.Features.Student.Commands.Create;
using LearningPlatform.Application.Features.Student.Commands.Update;
using LearningPlatform.Application.Features.Student.Queries.GetAll;
using LearningPlatform.Application.Features.Student.Queries.GetById;
using LearningPlatform.Application.Features.Student.Queries.GetStudentByEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.API.Controllers
{
    public class StudentController : ApiControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateStudentCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        //[Authorize(Roles = "User")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllStudentsQuery());
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetByIdStudentQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("getbyemail/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIdStudentByEmail(string email)
        {
            var query = new GetStudentByEmailQuery(email);
            var result = await Mediator.Send(query);
            return Ok(result);
        }



        [HttpPatch("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateStudentCommand command, Guid id)
        {
            if (command.StudentId != id)
            {
                return BadRequest("Id mismatch.");
            }

            // var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, "SameUserPolicy");
            /*
             if (!authorizationResult.Succeeded) return Forbid();
             */

            var result = await Mediator.Send(command);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

    }
}
