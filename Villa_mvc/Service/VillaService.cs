using System;
using MagicVilla_Unity;
using Villa_mvc.Model.VillaDTO;
using Villa_mvc.Models;
using Villa_mvc.Service.IService;
using static MagicVilla_Unity.SD;

namespace Villa_mvc.Service
{
    public class VillaService : BaseService,IVillaServics
    {
        private readonly IHttpClientFactory httpClient;
        private string VilllaUrl;
        public VillaService
            (IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            httpClient = httpClientFactory;
            VilllaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO obj, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.APIType.POST,
                Data = obj,
                Url = VilllaUrl + "api/v1/villa",
                Token = token
            });

        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.APIType.DELETE,
                Url = VilllaUrl + "api/v1/villa/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>( string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.APIType.GET,
                Url = VilllaUrl + "api/v1/villa",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.APIType.GET,
                Url = VilllaUrl + "api/v1/villa/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO obj, string token)
        {
            return SendAsync<T>(new APIRequest()    
            {
                ApiType = SD.APIType.PUT,
                Data = obj,
                Url = VilllaUrl + "api/v1/villa/" + obj.Id,
                Token = token
            });
        }
    }
}
