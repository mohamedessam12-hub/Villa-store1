using Microsoft.AspNetCore.Identity.Data;
using Villa_mvc.Models.VillaDTO;

namespace Villa_mvc.Service.IService
{
    public interface IUserService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO obj);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO obj);
    }
}
