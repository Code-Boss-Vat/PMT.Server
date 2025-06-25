
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMT.Core.RepositoryContracts;
using PMT.Infrastructure.Context;
using PMT.Infrastructure.Repositories;

namespace PMT.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>
            options.UseNpgsql(configuration.GetConnectionString("PMTDBConnection"))
            );

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IOrganizationsRepository, OrganizationsRepository>();
        services.AddScoped<ITeamsRepository, TeamsRepository>();
        services.AddScoped<ITasksRepository, TasksRepository>();

        return services;
    }
}