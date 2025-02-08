
using AdoptivePaws.Application.Services.Pet;
using AdoptivePaws.Application.Services.User;
using AdoptivePaws.Core.Interfaces.Pet;
using AdoptivePaws.Core.Interfaces.User;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using AdoptivePaws.Application.Mapper;

namespace AdoptivePaws.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI( this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddScoped<ILoginAppService, LoginAppService>();
            services.AddScoped<IUserAppService,UserAppService>();
            services.AddScoped<IPetAppService, PetAppService>();
            services.AddScoped<IAdoptionAppService,AdoptionAppService>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            return services;
        }
    }
}
