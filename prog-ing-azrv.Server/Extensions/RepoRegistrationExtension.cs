using prog_ing_azrv.Server.Implementations.Repos;
using prog_ing_azrv.Server.Interfaces.Repos;

namespace prog_ing_azrv.Server.Extensions
{
    public static class RepoRegistrationExtension
    {
        public static IServiceCollection RegisterRepositoryInterfaces(this IServiceCollection services) {

            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
