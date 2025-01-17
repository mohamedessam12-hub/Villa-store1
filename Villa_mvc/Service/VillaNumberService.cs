using System;
using MagicVilla_Unity;
using Villa_mvc.Model.VillaDTO;
using Villa_mvc.Service.IService;
using static MagicVilla_Unity.SD;

namespace Villa_mvc.Service
{
    public class VillaNumberService : BaseService, IVillaNumberServics
    {
        private readonly IHttpClientFactory httpClient;
        private string VilllaUrl;
        public VillaNumberService
            (IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            httpClient = httpClientFactory;
            VilllaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDTO obj, string token)
        {
            return SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.APIType.POST,
                Data = obj,
                Url = VilllaUrl + "api/v1/VillaNumber",
                Token = token
            });

        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.APIType.DELETE,
                Url = VilllaUrl + "api/v1/VillaNumber/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>( string token)
        {
            return SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.APIType.GET,
                Url = VilllaUrl + "api/v1/VillaNumber",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.APIType.GET,
                Url = VilllaUrl + "api/v1/VillaNumber/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO obj, string token)
        {
            return SendAsync<T>(new Models.APIRequest
            {
                ApiType = SD.APIType.PUT,
                Data = obj,
                Url = VilllaUrl + "api/v1/VillaNumber/" + obj.VillaNo,
                Token = token
            });
        }
    }
}
