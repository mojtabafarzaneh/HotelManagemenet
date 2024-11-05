using AutoMapper;
using Hotelmanagment.Api.Data;
using Hotelmanagment.Api.DTO;

namespace Hotelmanagment.Api.Configurations;

public class MapperConfig:Profile
{
    public MapperConfig()
    {
        CreateMap<Country, CountryDTO>().ReverseMap();
        CreateMap<Country,GetCountryDTO>().ReverseMap();
        CreateMap<Country, UpdateCountryDTO>().ReverseMap();
        CreateMap<Hotel, GetHotelDTO>().ReverseMap();
        CreateMap<Hotel,CreateHotelDTO>().ReverseMap();

        CreateMap<ApiUser, ApiUserDTO>().ReverseMap();
    }
    
}