using inventory_api.Application.Features.Auth.Commands;
using inventory_api.Application.Features.Auth.DTOs;
using inventory_api.Domain.Entities;
using inventory_api.Infastructure.JWT;
using inventory_api.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace inventory_api.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponseDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(ApplicationDbContext context, IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<LoginUserResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.LoginUserDto;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email, cancellationToken);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            if (!user.IsEmailConfirmed)
                throw new UnauthorizedAccessException("Please confirm your email before logging in");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result != PasswordVerificationResult.Success)
                throw new UnauthorizedAccessException("Invalid email or password");

            var token = _jwtService.GenerateToken(user);
            var expiration = DateTime.UtcNow.AddHours(1);

            //var roles = await _userManager.GetRolesAsync(user);

            return new LoginUserResponseDto
            {
                Token = token,
                TokenExpiration = expiration,
                UserId = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                //Roles = roles.ToList(),
                IsEmailConfirmed = user.IsEmailConfirmed
            };
        }
    }
}
