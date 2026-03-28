using PcBuilderBackend.Application.Auth.Dtos;
using PcBuilderBackend.Application.Common;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IAuthService
    {
        Task<IResult<AuthResponse>> Register(RegisterRequest request, CancellationToken ct = default);
        Task<IResult<AuthResponse>> Login(LoginRequest request, CancellationToken ct = default);
    }
}
