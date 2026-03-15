using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PcBuilderBackend.Domain.Interfaces;
using PcBuilderBackend.Persistence.Contexts;
using PcBuilderBackend.Persistence.UnitOfWork;

namespace PcBuilderBackend.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("'DefaultConnection' connection string is not configured.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IUnitOfWork, EFCoreUnitOfWork>();
            return services;
        }
    }
}
