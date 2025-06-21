using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PMT.Core.Validators;

namespace PMT.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<OrganizationCreateRequestValidator>();

        return services;
    }
}
