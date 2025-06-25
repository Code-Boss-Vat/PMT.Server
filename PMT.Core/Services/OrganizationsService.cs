using ApplicationCore.DTOs;
using AutoMapper;
using PMT.Core.DTOs;
using PMT.Core.Entities;
using PMT.Core.Helpers;
using PMT.Core.RepositoryContracts;
using PMT.Core.ServiceContracts;

namespace PMT.Core.Services;

public class OrganizationsService(IOrganizationsRepository _organizationsRepository, ITeamsRepository _teamsRepository, IUsersRepository _usersRepository, IMapper _mapper) : IOrganizationsService
{
    public async Task<OperationResult<OrganizationResponse?>> CreateOrganizationAsync(OrganizationCreateRequest? organizationCreateRequest)
    {
        OperationType operation = OperationType.Create;
        if (organizationCreateRequest == null)
        {
            return OperationResult.Failure<OrganizationResponse?>(operation, message: "Invalid organization creation request!");
        }
        if (!ValidationHelper.TryValidate(organizationCreateRequest, out var validationResults))
        {
            return OperationResult.Failure<OrganizationResponse?>(operation, message: validationResults.First().ErrorMessage);
        }

        Organization? organizationToAdd = _mapper.Map<Organization>(organizationCreateRequest);
        Organization? addedOrganization = await _organizationsRepository.AddOrganizationAsync(organizationToAdd);

        if (addedOrganization == null)
        {
            return OperationResult.Failure<OrganizationResponse?>(operation, message: "Organization creation failed!");
        }

        OrgAdminMap? orgAdminMap = _mapper.Map<OrgAdminMap>(addedOrganization);
        OrgAdminMap? addedOrgAdminMap = await _organizationsRepository.CreateOrgAdminMapAsync(orgAdminMap);

        if (addedOrgAdminMap == null)
        {
            return OperationResult.Failure<OrganizationResponse?>(operation, message: "Org Admin Map creation failed!");
        }

        OrganizationResponse? organizationResponse = _mapper.Map<OrganizationResponse>(addedOrganization);

        Team? defaultCreatedTeam = await this.CreateDefaultOrganizationTeam(addedOrganization);

        if (defaultCreatedTeam != null)
        {
            bool isAdminSetToDefaultTeam = await _usersRepository.SetUserTeamAsync(addedOrganization.CreatedBy, defaultCreatedTeam.Id);

            if (!isAdminSetToDefaultTeam)
            {
                return OperationResult.Failure<OrganizationResponse?>(operation, message: "Failed to set default team.");
            }
        }
        else
        {
            return OperationResult.Failure<OrganizationResponse?>(operation, message: "Failed to create default team.");
        }

        return OperationResult.Success(organizationResponse, operation, message: "Organization successfully created.")!;
    }

    public async Task<OperationResult<OrganizationResponse?>> UpdateOrganizationAsync(OrganizationUpdateRequest? organizationUpdateRequest)
    {
        OperationType operation = OperationType.Update;
        if (organizationUpdateRequest == null)
        {
            return OperationResult.Failure<OrganizationResponse?>(operation, message: "Invalid organization update request!");
        }
        if (!ValidationHelper.TryValidate(organizationUpdateRequest, out var validationResults))
        {
            return OperationResult.Failure<OrganizationResponse?>(operation, message: validationResults.First().ErrorMessage);
        }

        Organization? organizationToUpdate = _mapper.Map<Organization>(organizationUpdateRequest);
        Organization? updatedOrganization = await _organizationsRepository.UpdateOrganizationAsync(organizationToUpdate);

        if (updatedOrganization == null)
        {
            return OperationResult.Failure<OrganizationResponse?>(operation, message: "Organization update failed!");
        }

        OrganizationResponse? organizationResponse = _mapper.Map<OrganizationResponse>(updatedOrganization);

        return OperationResult.Success(organizationResponse, operation, message: "Organization updated successfully!")!;
    }

    private async Task<Team?> CreateDefaultOrganizationTeam(Organization organization)
    {
        Team team = new Team();
        team.Name = organization.Name + " Team";
        team.OrganizationId = organization.Id;
        team.CreatedBy = team.UpdatedBy = organization.CreatedBy;
        team.CreatedOn = team.UpdatedOn = DateTime.Now;

        Team? addedTeam = await _teamsRepository.AddTeamAsync(team);

        return addedTeam;
    }

    public async Task<OrganizationResponse?> GetUserOrganizationAsync(Guid? userId)
    {
        if (userId == null) return null;

        Organization? organization = await _organizationsRepository.GetUserOrganizationAsync(userId.Value);

        return _mapper.Map<OrganizationResponse>(organization);
    }
}
