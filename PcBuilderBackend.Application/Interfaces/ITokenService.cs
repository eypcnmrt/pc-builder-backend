using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
