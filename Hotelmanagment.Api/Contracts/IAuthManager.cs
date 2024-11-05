using Hotelmanagment.Api.DTO;
using Microsoft.AspNetCore.Identity;

namespace Hotelmanagment.Api.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(ApiUserDTO userDto);
}