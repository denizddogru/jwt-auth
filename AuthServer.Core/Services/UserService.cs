using AuthServer.Core.Dtos;
using AuthServer.Core.Entity;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Dtos;

namespace AuthServer.Core.Services;
public class UserService : IUserService
{

    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto userDto)
    {
        var user = new AppUser { Email = userDto.Email, UserName = userDto.UserName };

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if(!result.Succeeded)
        {
            var errors = result.Errors.Select(x=>x.Description).ToList();

            return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
        }
        
        return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
    }

    public Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
    {
        throw new NotImplementedException();
    }
}
