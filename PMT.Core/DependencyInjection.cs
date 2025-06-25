
using Microsoft.Extensions.DependencyInjection;
using PMT.Core.ServiceContracts;
using PMT.Core.Services;

namespace PMT.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IOrganizationsService, OrganizationsService>();
        services.AddScoped<ITasksService,  TasksService>();
        services.AddScoped<JwtService>();

        return services;
    }
}
