using AutoMapper;
using Hotelmanagment.Api.Contracts;
using Hotelmanagment.Api.Data;
using Hotelmanagment.Api.DTO;
using Microsoft.AspNetCore.Identity;

namespace Hotelmanagment.Api.Repository;

public class AuthManager: IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;
    public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
    {
        this._mapper = mapper;
        this._userManager = userManager;
    }
    public async Task<IEnumerable<IdentityError>> Register(ApiUserDTO userDto)
    {
        var user = _mapper.Map<ApiUser>(userDto);
        user.UserName = userDto.Email;
        
        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
        }
        return result.Errors;
    }
}