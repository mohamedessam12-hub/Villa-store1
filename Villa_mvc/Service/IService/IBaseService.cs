using Villa_mvc.Model;
using Villa_mvc.Models;

namespace Villa_mvc.Service.IService
{
    public interface IBaseService
    {
        APIResponse ResponseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
