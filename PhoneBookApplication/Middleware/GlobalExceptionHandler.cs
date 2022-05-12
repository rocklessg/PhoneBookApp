using Newtonsoft.Json;

namespace PhoneBookApplication.Middleware
{
    public class GlobalExceptionHandler
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
