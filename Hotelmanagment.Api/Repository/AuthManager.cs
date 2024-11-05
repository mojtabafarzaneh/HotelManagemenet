using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Hotelmanagment.Api.Contracts;
using Hotelmanagment.Api.Data;
using Hotelmanagment.Api.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Hotelmanagment.Api.Repository;

public class AuthManager : IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;
    private readonly IConfiguration _config;

    public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration config)
    {
        this._config = config;
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

    public async Task<AuthResponseDTO> Login(LoginDTO loginDto)
    {
        bool isValid = false; 
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (user is null || !isValid)
        {
            return null;
        }

        var token = await GenerateJwtTokenAsync(user);

        return new AuthResponseDTO
        {
            Token = token,
            UserId = user.Id
        };


    }

    private async Task<string> GenerateJwtTokenAsync(ApiUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);
        
        var claims =new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid",user.Id )
        }.Union(userClaims).Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer:_config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims:claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"])),
            signingCredentials: credentials
            );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}