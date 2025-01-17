using System.Net;

namespace Villa_mvc.Model
{
    public class APIResponse
    {
       
        public HttpStatusCode statusCode { get; set; }
        public bool isSuccess { get; set; } = true;
        public List<string> ErorMassege { get; set; }
        public object Result { get; set; }
    }
}
