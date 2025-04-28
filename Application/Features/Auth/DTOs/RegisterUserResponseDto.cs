namespace inventory_api.Application.Features.Auth.DTOs
{
    public class RegisterUserResponseDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? ContactNumber { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string? CompanyAddress { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
