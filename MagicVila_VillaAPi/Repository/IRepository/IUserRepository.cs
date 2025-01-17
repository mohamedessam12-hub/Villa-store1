using MagicVila_VillaAPi.Model;
using MagicVila_VillaAPi.Model.VillaDTO;

namespace MagicVila_VillaAPi.Repository.IRepository
{
    public interface IUserRepository
    {
        bool isUniqUser(string username);
        Task<LoginResponceDTO> Login(LoginRequestDTO loginRequest);
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequest);
        Task<bool> ConfirmEmailAsync(ConfirmEmailDTO confirmEmail);
        Task<bool> ConfirmPhoneAsync(ConfirmPhoneDTO  confirmPhone);

    }
}
