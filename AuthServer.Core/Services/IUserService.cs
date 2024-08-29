using AuthServer.Core.Dtos;
using SharedLibrary.Dtos;

namespace AuthServer.Core.Services;
public interface IUserService
{
    Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto userDto);
    Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
}
