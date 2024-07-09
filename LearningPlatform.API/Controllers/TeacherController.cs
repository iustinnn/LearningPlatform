using LearningPlatform.Application.Features.Student.Commands.Update;
using LearningPlatform.Application.Features.Student.Queries.GetById;
using LearningPlatform.Application.Features.Student.Queries.GetStudentByEmail;
using LearningPlatform.Application.Features.Teacher.Commands.Create;
using LearningPlatform.Application.Features.Teacher.Commands.Update;
using LearningPlatform.Application.Features.Teacher.Queries.GetAll;
using LearningPlatform.Application.Features.Teacher.Queries.GetById;
using LearningPlatform.Application.Features.Teacher.Queries.GetTeacherByEmail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearningPlatform.API.Controllers
{
    public class TeacherController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateTeacherCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /*
                [HttpGet]
                [ProducesResponseType(StatusCodes.Status200OK)]
                public async Task<IActionResult> GetAll()
                {
                    var result = await Mediator.Send(new GetAllTeachersQuery());
                    return Ok(result);
                }

                [HttpGet("{id:guid}")]
                [ProducesResponseType(StatusCodes.Status200OK)]
                [ProducesResponseType(StatusCodes.Status404NotFound)]
                public async Task<IActionResult> GetById(Guid id)
                {
                    var query = new GetByIdTeacherQuery(id);
                    var result = await Mediator.Send(query);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);
                }
            }
        */



        [HttpPatch("{id:guid}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateTeacherCommand command, Guid id)
        {/*
            if (command.PatientId != id)
            {
                return BadRequest("Id mismatch.");
            }

           // var authorizationResult = await _authorizationService.AuthorizeAsync(User, id, "SameUserPolicy");

            if (!authorizationResult.Succeeded) return Forbid();
            */

            var result = await Mediator.Send(command);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllTeachersQuery());
            return Ok(result);
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetByIdTeacherQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("getbyemail/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIdTeacherByEmail(string email)
        {
            var query = new GetTeacherByEmailQuery(email);
            var result = await Mediator.Send(query);
            return Ok(result);
        }


    }
}
