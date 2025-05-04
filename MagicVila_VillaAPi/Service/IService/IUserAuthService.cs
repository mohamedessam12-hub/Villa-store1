using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;

namespace MagicVila_VillaAPi.Service.IService
{
    public interface IUserAuthService
    {
        Task<APIResponse> LoginAsync(LoginRequestDTO model);
        Task<APIResponse> RegisterAsync(RegisterationRequestDTO registerRequest);
        Task<APIResponse> ConfirmEmailAsync(ConfirmEmailDTO userDto);
        Task<APIResponse> ConfirmPhoneAsync(ConfirmPhoneDTO userDto);
    }

}
