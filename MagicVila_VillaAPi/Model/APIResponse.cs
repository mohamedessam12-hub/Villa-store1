using System.Net;

namespace MagicVila_VillaAPi.Model
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErorMassege = new List<string>();
        }

        public HttpStatusCode statusCode { get; set; }
        public bool isSuccess { get; set; }
        public List<string> ErorMassege { get; set; }
        public object Result { get; set; }
    }
}
