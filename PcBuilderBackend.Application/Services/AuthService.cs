using Microsoft.Extensions.Logging;
using PcBuilderBackend.Application.Auth.Dtos;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService, ILogger<AuthService> logger)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<IResult<AuthResponse>> Register(RegisterRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<User>();

                if (await repo.AnyAsync(u => u.Email == request.Email, ct))
                    return Result<AuthResponse>.Error("Bu e-posta adresi zaten kullanılıyor.");

                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = _passwordHasher.Hash(request.Password),
                    CreatedAt = DateTime.UtcNow
                };

                await repo.AddAsync(user, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                var token = _tokenService.GenerateToken(user);
                return Result<AuthResponse>.Created(
                    new AuthResponse(token, user.Username, user.Email),
                    "Kayıt başarılı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Register));
                return Result<AuthResponse>.Error("Bir hata oluştu.");
            }
        }

        public async Task<IResult<AuthResponse>> Login(LoginRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<User>();
                var user = await repo.FirstOrDefaultAsync(u => u.Email == request.Email, ct);

                if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
                    return Result<AuthResponse>.NotFound("Geçersiz e-posta veya şifre.");

                var token = _tokenService.GenerateToken(user);
                return Result<AuthResponse>.Ok(new AuthResponse(token, user.Username, user.Email));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Login));
                return Result<AuthResponse>.Error("Bir hata oluştu.");
            }
        }
    }
}
