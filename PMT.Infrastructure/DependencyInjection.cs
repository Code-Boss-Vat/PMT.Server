
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMT.Infrastructure.Context;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace PMT.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDBContext>(
            options =>
            options.UseNpgsql(configuration.GetConnectionString("PMTDBConnection"))
            );


        return services;
    }
}