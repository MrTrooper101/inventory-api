namespace inventory_api.Application.Features.Auth.DTOs
{
    public class LoginUserResponseDto
    {
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        //public List<string> Roles { get; set; } = new();
        public bool IsEmailConfirmed { get; set; }
    }
}
