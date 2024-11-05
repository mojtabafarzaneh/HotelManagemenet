using Hotelmanagment.Api.Contracts;
using Hotelmanagment.Api.Data;

namespace Hotelmanagment.Api.Repository;

public class HotelsRepository: GenericRepository<Hotel>, IHotelsRepository
{
    private readonly HotelManagmentDBContext _context;
    public HotelsRepository(HotelManagmentDBContext dbContext) : base(dbContext)
    {
        _context = dbContext;
        
    }

    public async Task<Hotel> AddHotelAsync(Hotel hotel)
    {
        var country = await _context.Countries.FindAsync(hotel.CountryId);
        if (country is null)
        {
            return null;
        }
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }

}