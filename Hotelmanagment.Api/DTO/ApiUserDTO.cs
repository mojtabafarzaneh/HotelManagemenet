using System.ComponentModel.DataAnnotations;

namespace Hotelmanagment.Api.DTO;

public class ApiUserDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName  { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}