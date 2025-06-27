using Microsoft.EntityFrameworkCore;
using PMT.Core.Entities;
using PMT.Core.RepositoryContracts;
using PMT.Infrastructure.Context;

namespace PMT.Infrastructure.Repositories;

public class OrganizationsRepository(ApplicationDbContext _dbContext) : IOrganizationsRepository
{
    public async Task<Organization?> AddOrganizationAsync(Organization organization)
    {
        if (organization == null) return null;
        if (organization.Id != Guid.Empty) return null;

        organization.Id = Guid.NewGuid();

        await _dbContext.Organizations.AddAsync(organization);

        int recordsAffected = await _dbContext.SaveChangesAsync();

        if (recordsAffected > 0)
        {
            return organization;
        }
        return null;
    }

    public async Task<Organization?> UpdateOrganizationAsync(Organization organization)
    {
        if (organization == null) return null;
        if (organization.Id == Guid.Empty) return null;

        Organization? existingOrganization = await _dbContext.Organizations.FirstOrDefaultAsync(org => org.Id == organization.Id);

        if (existingOrganization == null) return null;

        existingOrganization.Name = organization.Name;
        existingOrganization.Email = organization.Email;
        existingOrganization.PhoneNumber = organization.PhoneNumber;
        existingOrganization.Address = organization.Address;
        existingOrganization.Website = organization.Website;
        existingOrganization.UpdatedBy = organization.UpdatedBy;
        existingOrganization.UpdatedOn = organization.UpdatedOn;

        int rowsAffected = await _dbContext.SaveChangesAsync();

        if (rowsAffected > 0)
        {
            return existingOrganization;
        }
        return null;
    }

    public async Task<bool> DeleteOrganizationAsync(Guid? organizationId)
    {
        if (organizationId == null || organizationId == Guid.Empty) throw new ArgumentNullException("Invalid OrganizationId. Delete operation failed");
        Organization? existingOrganization = await _dbContext.Organizations.FirstOrDefaultAsync(org => org.Id == organizationId);

        if (existingOrganization == null) throw new ArgumentNullException("Organization not found. Delete operation failed.");

        _dbContext.Organizations.Remove(existingOrganization);
        int recordsAffected = await _dbContext.SaveChangesAsync();

        return (recordsAffected > 0);
    }

    public async Task<OrgAdminMap?> CreateOrgAdminMapAsync(OrgAdminMap orgAdminMap)
    {
        if (orgAdminMap == null) throw new ArgumentNullException("Invalid request.");
        Organization? existingOrganization = await _dbContext.Organizations.FirstOrDefaultAsync(org => org.Id == orgAdminMap.OrganizationId);

        if (existingOrganization == null) throw new ArgumentNullException("Organization not found. Set operation failed.");

        ApplicationUser? existingUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == orgAdminMap.AdminId);

        if (existingUser == null) throw new ArgumentNullException("Application User not found. Set operation failed.");

        _dbContext.OrgAdminMaps.Add(orgAdminMap);
        int recordsAffected = await _dbContext.SaveChangesAsync();

        if (recordsAffected > 0)
        {
            return orgAdminMap;
        }

        return null;
    }

    public async Task<Organization?> GetUserOrganizationAsync(Guid userId)
    {
        Organization? organization = await (
            from user in _dbContext.Users
            join team in _dbContext.Teams
            on user.TeamId equals team.Id
            join org in _dbContext.Organizations
            on team.OrganizationId equals org.Id
            where user.Id == userId
            select org)
            .FirstOrDefaultAsync();

        return organization;
    }

    public async Task<List<Organization>> GetAllOrganizationsAsync()
    {
        return await _dbContext.Organizations.ToListAsync() ?? new List<Organization>();
    }
}