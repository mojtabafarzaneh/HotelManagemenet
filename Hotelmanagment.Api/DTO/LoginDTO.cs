using System.ComponentModel.DataAnnotations;

namespace Hotelmanagment.Api.DTO;

public class LoginDTO
{
    public string Email { get; set; }
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}