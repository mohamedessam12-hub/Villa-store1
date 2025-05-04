using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;
using MagicVila_VillaAPi.Repository.IRepository;
using MagicVila_VillaAPi.Service.IService;
using System.Net;

namespace MagicVila_VillaAPi.Service
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserRepository _userRepository;

        public UserAuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<APIResponse> LoginAsync(LoginRequestDTO model)
        {
            var response = new APIResponse();
            var loginResult = await _userRepository.Login(model);

            if (loginResult == null || loginResult.User == null || string.IsNullOrEmpty(loginResult.Token))
            {
                response.statusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErorMassege.Add("User Name And Password Is Incorrect");
            }
            else
            {
                response.statusCode = HttpStatusCode.OK;
                response.isSuccess = true;
                response.Result = loginResult;
            }

            return response;
        }

        public async Task<APIResponse> RegisterAsync(RegisterationRequestDTO registerRequest)
        {
            var response = new APIResponse();

            if (!_userRepository.isUniqUser(registerRequest.UserName))
            {
                response.statusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErorMassege.Add("UserName already exists");
                return response;
            }

            var user = await _userRepository.Register(registerRequest);
            if (user == null)
            {
                response.statusCode = HttpStatusCode.BadRequest;
                response.isSuccess = false;
                response.ErorMassege.Add("Error during registration");
                return response;
            }

            response.statusCode = HttpStatusCode.OK;
            response.isSuccess = true;
            response.Result = user;
            return response;
        }

        public async Task<APIResponse> ConfirmEmailAsync(ConfirmEmailDTO userDto)
        {
            var result = await _userRepository.ConfirmEmailAsync(userDto);
            return new APIResponse
            {
                statusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                isSuccess = result,
                Result = result ? "Email confirmed successfully." : null,
                ErorMassege = result ? null : new List<string> { "Email confirmation failed." }
            };
        }

        public async Task<APIResponse> ConfirmPhoneAsync(ConfirmPhoneDTO userDto)
        {
            var result = await _userRepository.ConfirmPhoneAsync(userDto);
            return new APIResponse
            {
                statusCode = result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                isSuccess = result,
                Result = result ? "Phone confirmed successfully." : null,
                ErorMassege = result ? null : new List<string> { "Phone confirmation failed." }
            };
        }
    }

}
