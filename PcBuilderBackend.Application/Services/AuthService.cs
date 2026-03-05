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

        public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<IResult<AuthResponse>> Kayit(RegisterRequest request, CancellationToken ct = default)
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
                return Result<AuthResponse>.Error(ex.Message);
            }
        }

        public async Task<IResult<AuthResponse>> Giris(LoginRequest request, CancellationToken ct = default)
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
                return Result<AuthResponse>.Error(ex.Message);
            }
        }
    }
}
