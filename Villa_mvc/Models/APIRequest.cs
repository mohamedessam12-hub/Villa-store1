using static MagicVilla_Unity.SD;

namespace Villa_mvc.Models
{
    public class APIRequest
    {
        public APIType ApiType { get; set; } = APIType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
