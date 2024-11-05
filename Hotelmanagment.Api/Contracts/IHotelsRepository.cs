using Hotelmanagment.Api.Data;

namespace Hotelmanagment.Api.Contracts;

public interface IHotelsRepository: IGenericRepository<Hotel>
{
    public Task<Hotel> AddHotelAsync(Hotel hotel);
    
}