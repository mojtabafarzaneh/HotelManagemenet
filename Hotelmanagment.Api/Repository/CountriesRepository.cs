using Hotelmanagment.Api.Contracts;
using Hotelmanagment.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotelmanagment.Api.Repository;

public class CountriesRepository: GenericRepository<Country>, ICountriesRepository
{
    private readonly HotelManagmentDBContext _context;
    public CountriesRepository(HotelManagmentDBContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Country> GetCountryAsync(int id)
    {
        return await _context.Countries.Include(q => q.Hotels)
            .FirstOrDefaultAsync(q => q.Id == id);
        
    }
    
}