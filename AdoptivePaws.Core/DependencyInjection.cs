using AdoptivePaws.Core.Common;
using AdoptivePaws.Core.Interfaces.Pet;
using AdoptivePaws.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdoptivePaws.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.SectionName));
            // services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.SectionName));
            services.AddScoped<ICommonAppService, CommonAppService>();
            return services;
        }
    }

}
