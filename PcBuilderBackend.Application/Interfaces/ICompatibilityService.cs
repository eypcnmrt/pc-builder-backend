using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Compatibility.Dtos;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface ICompatibilityService
    {
        Task<IResult<CompatibilityCheckResult>> Check(CompatibilityCheckRequest request, CancellationToken ct = default);
    }
}
