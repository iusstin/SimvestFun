using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using SimvestFun.ApplicationCore.Interfaces;
using SimvestFun.ApplicationCore.Models;

namespace SimvestFun.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService,
                              IJwtUtils jwtUtils) : base(jwtUtils)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserModel>> AuthenticateWithPassword([FromBody] AuthenticateRequest model)
        {
            try
            {
                var response = await _authService.AuthenticateWithPasswordAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterWithPassword([FromBody] RegisterRequest model)
        {
            try
            {
                if(model.Name.Length > 30 || model.Email.Length > 100)
                    return BadRequest();

                await _authService.RegisterWithPasswordAsync(model);
                return Ok(new { message = "Registration successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate/google")]
        public async Task<ActionResult<UserModel>> GoogleAuthenticate([FromBody] string idToken)
        {
            try
            {
                var userResponse = await _authService.AuthenticateWithGoogleAsync(idToken);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register/google")]
        public async Task<ActionResult<UserModel>> GoogleRegister([FromBody] string idToken)
        {
            try
            {
                var authUser = await _authService.RegisterWithGoogleAsync(idToken);
                return Ok(authUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate/facebook")]
        public async Task<ActionResult<UserModel>> FacebookAuhenticate([FromBody] string authToken)
        {
            try
            {
                var userModel = await _authService.AuthenticateWithFacebookAsync(authToken);
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register/facebook")]
        public async Task<ActionResult<UserModel>> FacebookRegister([FromBody] string authToken)
        {
            try
            {
                var authUser = await _authService.RegisterWithFacebookAsync(authToken);
                return Ok(authUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            try
            {
                string referer = Request.Headers["Referer"].ToString();

                if (await _authService.SendForgotPasswordEmail(model.Email, referer))
                {
                    return Ok();
                }
                else 
                {
                    return BadRequest();
                }
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                await _authService.ResetPassword(model);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
    }
}