using LearningPlatform.API.Models;
using LearningPlatform.Application.Contracts.Identity;
using LearningPlatform.Application.Contracts.Interfaces;
using LearningPlatform.Application.Models.Identity;
using LearningPlatform.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace LearningPlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ICurrentUserService _currentUserService;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Login(model);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register/student")]
        public async Task<IActionResult> RegisterStudent(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.RegisterationStudent(model, UserRoles.Student);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return CreatedAtAction(nameof(RegisterStudent), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("register/teacher")]
        public async Task<IActionResult> RegisterTeacher(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.RegisterationTeacher(model, UserRoles.Teacher);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return CreatedAtAction(nameof(RegisterTeacher), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok();
        }


        [HttpGet]
        [Route("currentuserinfo")]
        public CurrentUser CurrentUserInfo()
        {
      
            var currentUserName = this._currentUserService.GetCurrentUserName();

            var currentClaimsPrincipal = _currentUserService.GetCurrentClaimsPrincipal();

            if(currentClaimsPrincipal == null || currentUserName == null)
            {
                return new CurrentUser
                {
                    IsAuthenticated = false
                };
            }

            return new CurrentUser
            {
                IsAuthenticated = true,
                UserName = currentUserName,
                UserId = this._currentUserService.GetCurrentUserId(),
               
                Claims = this._currentUserService.GetCurrentClaimsPrincipal().Claims.ToDictionary(c => c.Type, c => c.Value)

            };
        }

    }
}

