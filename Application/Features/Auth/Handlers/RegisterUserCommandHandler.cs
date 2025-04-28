using AutoMapper;
using inventory_api.Application.Features.Auth.Commands;
using inventory_api.Application.Features.Auth.DTOs;
using inventory_api.Domain.Entities;
using inventory_api.Infastructure.Email;
using inventory_api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace inventory_api.Application.Features.Auth.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponseDto>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public RegisterUserCommandHandler(ApplicationDbContext context, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<RegisterUserResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.RegisterUserDto.Email, cancellationToken);

            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }

            var user = new User
            {
                Email = request.RegisterUserDto.Email,
                FirstName = request.RegisterUserDto.FirstName,
                MiddleName = request.RegisterUserDto.MiddleName,
                LastName = request.RegisterUserDto.LastName,
                ContactNumber = request.RegisterUserDto.ContactNumber,
                CompanyName = request.RegisterUserDto.CompanyName,
                CompanyAddress = request.RegisterUserDto.CompanyAddress,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            await _emailService.SendEmailAsync(user.Email,
                "Verify your email",
                $"Please verify your email by clicking here: " +
                $"https://yourfrontend.com/verify-email?token={user.EmailConfirmationToken}", cancellationToken);

            return _mapper.Map<RegisterUserResponseDto>(user);
        }
    }
}
