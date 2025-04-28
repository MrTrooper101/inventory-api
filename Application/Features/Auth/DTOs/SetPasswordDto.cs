namespace inventory_api.Application.Features.Auth.DTOs
{
    public class SetPasswordDto
    {
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
