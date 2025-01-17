using MagicVilla_Unity;
using Villa_mvc.Models;
using Villa_mvc.Models.VillaDTO;
using Villa_mvc.Service.IService;

namespace Villa_mvc.Service
{
    public class UserService : BaseService,IUserService
    {
        private readonly IHttpClientFactory httpClient;
        private string VilllaUrl;
        public UserService (IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            httpClient = httpClientFactory;
            VilllaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.APIType.POST,
                Data = obj,
                Url = VilllaUrl + "api/UserAuth/login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.APIType.POST,
                Data = obj,
                Url = VilllaUrl + "api/UserAuth/register"
            });
        }
    }
}
