
using PMT.Core.Entities;

namespace PMT.Core.RepositoryContracts;

public interface IOrganizationsRepository
{
    Task<Organization?> AddOrganizationAsync(Organization organization);
    Task<Organization?> UpdateOrganizationAsync(Organization organization);
    Task<bool> DeleteOrganizationAsync(Guid? organizationId);
    Task<OrgAdminMap?> CreateOrgAdminMapAsync(OrgAdminMap orgAdminMap);
    Task<Organization?> GetUserOrganizationAsync(Guid userId);
    Task<List<Organization>> GetAllOrganizationsAsync();
}

