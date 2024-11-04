namespace Hotelmanagment.Api.DTO;

public class GetCountryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<GetHotelDTO> Hotels {get; set;}
}