using Microsoft.Extensions.DependencyInjection;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Application.Services;
using PcBuilderBackend.Infrastructure.Services;

namespace PcBuilderBackend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProcessorService, ProcessorService>();
            services.AddScoped<IMotherboardService, MotherboardService>();
            services.AddScoped<IGpuService, GpuService>();
            services.AddScoped<IRamService, RamService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IPsuService, PsuService>();
            services.AddScoped<IPcCaseService, PcCaseService>();
            services.AddScoped<ICoolerService, CoolerService>();
            services.AddScoped<ICompatibilityService, CompatibilityService>();
            services.AddScoped<IBuildService, BuildService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
            return services;
        }
    }
}
