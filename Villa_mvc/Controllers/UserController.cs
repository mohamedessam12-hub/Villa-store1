using MagicVilla_Unity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Villa_mvc.Service.IService;
using Microsoft.AspNetCore.Identity.Data;
using Villa_mvc.Models.VillaDTO;
using Villa_mvc.Model;
using System.IdentityModel.Tokens.Jwt;

namespace Villa_mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _authService;
        public UserController(IUserService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO obj = new();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO obj)
        {
            APIResponse response = await _authService.LoginAsync<APIResponse>(obj);
            if (response != null && response.isSuccess)
            {
                LoginResponceDTO model = JsonConvert.DeserializeObject<LoginResponceDTO>(Convert.ToString(response.Result));

                var handeler = new JwtSecurityTokenHandler();
                var jwt = handeler.ReadJwtToken(model.Token);

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name,model.User.Name));
                identity.AddClaim(new Claim(ClaimTypes.Role,jwt.Claims.FirstOrDefault(u => u.Type =="role").Value));
                var principle = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);


                HttpContext.Session.SetString(SD.SessionToken, model.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", response.ErorMassege.FirstOrDefault());
                return View(obj);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterationRequestDTO obj)
        {
            APIResponse result = await _authService.RegisterAsync<APIResponse>(obj);
            if (result != null && result.isSuccess)
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, "");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
