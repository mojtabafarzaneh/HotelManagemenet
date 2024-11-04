namespace Hotelmanagment.Api.DTO;

public class GetHotelDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Rating { get; set; }
    
    public int CountryId { get; set; }
}