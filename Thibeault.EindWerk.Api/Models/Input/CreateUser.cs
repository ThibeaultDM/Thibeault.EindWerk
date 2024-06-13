namespace Thibeault.EindWerk.Api.Models.Input
{
    public class CreateUser
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirm { get; set; }

    }
}
