using System.ComponentModel.DataAnnotations;

namespace Hotelmanagment.Api.DTO;

public class CreateHotelDTO
{
    [Required]
    public string Name { get; set; }
    public string Address { get; set; }
    public string Rating { get; set; }
    [Required]
    public int CountryId { get; set; }
}