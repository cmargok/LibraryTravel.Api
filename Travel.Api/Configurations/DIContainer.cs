using Travel.Application.Infra_Contracts;
using Travel.Application.Services;
using Travel.Application.Services.Contracts;
using Travel.Infrastructure.Persistence.Repositories;

namespace Travel.Api.Configurations
{
    public static class DIContainer
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection Services)
        {
            Services.AddScoped<ILibroRepository, LibroRepository>();
            Services.AddScoped<IAutorRepository, AutorRepository>();
            Services.AddScoped<IEditorialRepository, EditorialRepository>();

            return Services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection Services)
        {
            Services.AddScoped<ILibrosManager, LibrosManager>();
            Services.AddScoped<IAutorManager, AutorManager>();
            Services.AddScoped<IEditorialManager, EditorialManager>();
            return Services;
        }
    }
}
