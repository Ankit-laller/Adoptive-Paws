using AdoptivePaws.Core.Interfaces.Pet;
using AdoptivePaws.Core.Interfaces.User;
using AdoptivePaws.Core.Options;
using AdoptivePaws.Infrastructure.Data;
using AdoptivePaws.Infrastructure.Repositories.Pet;
using AdoptivePaws.Infrastructure.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptivePaws.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>((provider,options) =>
            {
                options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection);
            });
            services.AddScoped<IUserRepository, UserRepoistory>();
            services.AddScoped<IPetReposistory, PetRepository>();
            return services;
        }
    }
}
