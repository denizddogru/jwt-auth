using AuthServer.Core.Dtos;
using AuthServer.Core.Entity;
using AuthServer.Service;
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

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();

            return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
        }

        // user, userAppDto'ya maplenicek yani user nesnesinini userAppDto türündeki bir nesneye dönüştürülmesini ifade eder.
        return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
    }

    public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if(user == null)
        {
            return Response<UserAppDto>.Fail("Username not found", 404, true);
        }

        return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
    }
}
