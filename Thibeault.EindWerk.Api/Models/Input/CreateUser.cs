namespace Thibeault.EindWerk.Api.Models.Input
{
    public class CreateUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}