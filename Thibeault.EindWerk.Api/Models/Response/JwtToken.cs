namespace Thibeault.EindWerk.Api.Models.Response
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}