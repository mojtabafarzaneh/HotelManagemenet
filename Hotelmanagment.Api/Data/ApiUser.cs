using Microsoft.AspNetCore.Identity;

namespace Hotelmanagment.Api.Data;

public class ApiUser: IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}