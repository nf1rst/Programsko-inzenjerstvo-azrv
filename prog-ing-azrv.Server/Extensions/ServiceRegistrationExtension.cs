using prog_ing_azrv.Server.Implementations.Services;
using prog_ing_azrv.Server.Interfaces.Services;
using System.Runtime.CompilerServices;

namespace prog_ing_azrv.Server.Extensions
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection RegisterServiceAbstractions(this IServiceCollection services)
        {
            services.AddScoped<IUserSErvice, UserService>();
            services.AddScoped<ISkillService, SkillService>();

            return services;
        }
            
    }
}
