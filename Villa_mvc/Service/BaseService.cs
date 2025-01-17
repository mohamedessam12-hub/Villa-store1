using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Villa_mvc.Model;
using Villa_mvc.Models;
using Villa_mvc.Service.IService;
using static MagicVilla_Unity.SD;

namespace Villa_mvc.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public APIResponse ResponseModel {get; set; }
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.ResponseModel = new();
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {   
            try
            {
                var client = httpClientFactory.CreateClient("MagicVilla");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case APIType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case APIType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case APIType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case APIType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = null;

                if (!string.IsNullOrEmpty(apiRequest.Token)) 
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Response Content: " + apiContent);
                
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                Console.WriteLine(apiContent);
                
                
                return APIResponse;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse {isSuccess = false , ErorMassege = new List<string> { Convert.ToString(ex.Message) } };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
