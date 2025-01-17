using System.Net;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository;
using MagicVila_VillaAPi.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MagicVila_VillaAPi.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    [ApiVersionNeutral]
    public class UserAuthController : ControllerBase
    { 
        private readonly IUserRepository userRepository;
        protected APIResponse _responce;
        public UserAuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            _responce = new ();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            try
            {
                var loginresponce = await userRepository.Login(model);
                if (loginresponce == null || loginresponce.User == null || string.IsNullOrEmpty(loginresponce.Token))
                {
                    _responce.statusCode = HttpStatusCode.BadRequest;
                    _responce.isSuccess = false;
                    _responce.ErorMassege.Add("User Name And Password Is Incorrect");
                    return BadRequest(_responce);
                }

                _responce.statusCode = HttpStatusCode.OK;
                _responce.isSuccess = true;
                _responce.Result = loginresponce;
                return Ok(_responce);
            }
            catch (Exception ex)
            {
                _responce.statusCode = HttpStatusCode.InternalServerError;
                _responce.isSuccess = false;
                _responce.ErorMassege.Add($"An error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, _responce);
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO registerRequest)
        {
            try
            {
                bool useruniqe = userRepository.isUniqUser(registerRequest.UserName);
                if (!useruniqe)
                {
                    _responce.statusCode = HttpStatusCode.BadRequest;
                    _responce.isSuccess = false;
                    _responce.ErorMassege.Add("UserName already exists");
                    return BadRequest(_responce);
                }
                var user = await userRepository.Register(registerRequest);
                if (user == null)
                {
                    _responce.statusCode = HttpStatusCode.BadRequest;
                    _responce.isSuccess = false;
                    _responce.ErorMassege.Add("Error during registration");
                    return BadRequest(_responce);
                }
                _responce.statusCode = HttpStatusCode.OK;
                _responce.isSuccess = true;
                _responce.Result = user;
                return Ok(_responce);
            }
            catch (Exception ex)
            {
                _responce.statusCode = HttpStatusCode.BadRequest;
                _responce.isSuccess = false;
                _responce.ErorMassege.Add($"Error: {ex.Message}");
                return BadRequest(_responce);
            }
        }
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDTO userDto)
        {
            try
            {
                var result = await userRepository.ConfirmEmailAsync(userDto);
                return result ? Ok("Email confirmed successfully.") : BadRequest("Email confirmation failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("ConfirmPhone")]
        public async Task<IActionResult> ConfirmPhone([FromBody] ConfirmPhoneDTO userDto)
        {
            try
            {
                var result = await userRepository.ConfirmPhoneAsync(userDto);
                return result ? Ok("Phone confirmed successfully.") : BadRequest("Phone confirmation failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


    }
}
