using ApplicationCore.DTOs;
using PMT.Core.DTOs;

namespace PMT.Core.ServiceContracts;

public interface IOrganizationsService
{
    Task<OperationResult<OrganizationResponse?>> CreateOrganizationAsync(OrganizationCreateRequest? organizationCreateRequest);
    Task<OperationResult<OrganizationResponse?>> UpdateOrganizationAsync(OrganizationUpdateRequest? organizationUpdateRequest);

    Task<OrganizationResponse?> GetUserOrganizationAsync(Guid? userId);
}