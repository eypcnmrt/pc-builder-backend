using PcBuilderBackend.Application.Auth.Dtos;
using PcBuilderBackend.Application.Common;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<IResult<AuthResponse>> Kayit(RegisterRequest request, CancellationToken ct = default);
        Task<IResult<AuthResponse>> Giris(LoginRequest request, CancellationToken ct = default);
    }
}
