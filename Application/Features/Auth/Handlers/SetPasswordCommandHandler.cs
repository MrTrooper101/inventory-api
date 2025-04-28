using inventory_api.Application.Features.Auth.Commands;
using inventory_api.Domain.Entities;
using inventory_api.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace inventory_api.Application.Features.Auth.Handlers
{
    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public SetPasswordCommandHandler(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var dto = request.SetPasswordDto;

            if (dto.Password != dto.ConfirmPassword)
                throw new Exception("Passwords do not match.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailConfirmationToken == dto.Token, cancellationToken);

            if (user == null)   
                throw new Exception("Invalid or expired token.");

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            user.IsEmailConfirmed = true;
            user.EmailConfirmationToken = null;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
