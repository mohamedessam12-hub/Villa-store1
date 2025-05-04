using System.Net;
using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository;
using MagicVila_VillaAPi.Repository.IRepository;
using MagicVila_VillaAPi.Service.IService;
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
        private readonly IUserAuthService _authService;

        public UserAuthController(IUserAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var response = await _authService.LoginAsync(model);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDTO registerRequest)
        {
            var response = await _authService.RegisterAsync(registerRequest);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDTO userDto)
        {
            var response = await _authService.ConfirmEmailAsync(userDto);
            return StatusCode((int)response.statusCode, response);
        }

        [HttpPost("ConfirmPhone")]
        public async Task<IActionResult> ConfirmPhone([FromBody] ConfirmPhoneDTO userDto)
        {
            var response = await _authService.ConfirmPhoneAsync(userDto);
            return StatusCode((int)response.statusCode, response);
        }
    }

}
