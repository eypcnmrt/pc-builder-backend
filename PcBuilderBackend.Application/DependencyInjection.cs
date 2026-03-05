using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace PcBuilderBackend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
