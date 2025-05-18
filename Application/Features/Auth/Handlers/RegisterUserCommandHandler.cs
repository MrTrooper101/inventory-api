using AutoMapper;
using inventory_api.Application.Features.Auth.Commands;
using inventory_api.Application.Features.Auth.DTOs;
using inventory_api.Domain.Entities;
using inventory_api.Infastructure.Email;
using inventory_api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace inventory_api.Application.Features.Auth.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponseDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public RegisterUserCommandHandler(ApplicationDbContext context, IMapper mapper, IEmailService emailService, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<RegisterUserResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var email = request.RegisterUserDto.Email;
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            if (existingUser != null)
            {
                if (existingUser.IsEmailConfirmed)
                {
                    throw new Exception("User with this email already exists and is verified.");
                }

                var newToken = GenerateSecureToken();
                existingUser.EmailConfirmationToken = newToken;
                // existingUser.EmailConfirmationTokenExpiry = DateTime.UtcNow.AddHours(24);

                await _context.SaveChangesAsync(cancellationToken);

                await SendVerificationEmail(existingUser.Email, newToken, cancellationToken);

                return _mapper.Map<RegisterUserResponseDto>(existingUser);
            }

            var token = GenerateSecureToken();

            var newUser = new User
            {
                Email = request.RegisterUserDto.Email,
                FirstName = request.RegisterUserDto.FirstName,
                MiddleName = request.RegisterUserDto.MiddleName,
                LastName = request.RegisterUserDto.LastName,
                ContactNumber = request.RegisterUserDto.ContactNumber,
                CompanyName = request.RegisterUserDto.CompanyName,
                CompanyAddress = request.RegisterUserDto.CompanyAddress,
                EmailConfirmationToken = token,
                // EmailConfirmationTokenExpiry = DateTime.UtcNow.AddHours(24)
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);

            await SendVerificationEmail(newUser.Email, token, cancellationToken);

            return _mapper.Map<RegisterUserResponseDto>(newUser);
        }

        private static string GenerateSecureToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        private async Task SendVerificationEmail(string email, string token, CancellationToken cancellationToken)
        {
            var encodedToken = Uri.EscapeDataString(token);
            var baseUrl = _configuration["Frontend:BaseUrl"];
            var verificationLink = $"{baseUrl}/set-password?token={encodedToken}";
            var subject = "Verify your email";
            var body = $@"
                <p>Please verify your email by clicking the link below:</p>
                <p><a href=""{verificationLink}"">Verify Email</a></p>
                <p>If you didn’t request this, please ignore this email.</p>
            ";

            await _emailService.SendEmailAsync(email, subject, body, cancellationToken);
        }

    }
}
