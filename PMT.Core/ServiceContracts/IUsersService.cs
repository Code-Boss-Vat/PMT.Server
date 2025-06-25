using ApplicationCore.DTOs;
using PMT.Core.DTOs;

namespace PMT.Core.ServiceContracts;

public interface IUsersService
{
    Task<OperationResult<UserResponse?>> ValidateLoginCredentialsAsync(SignInRequest? loginRequest);
    Task<OperationResult<UserResponse?>> RegisterUserAsync(SignupRequest? registerRequest);
    Task<OperationResult<UserResponse?>> UpdateUserAsync(UserUpdateRequest? userUpdateRequest);
    Task<OperationResult<UserResponse?>> GetUserByUserName(string? userName);
}
