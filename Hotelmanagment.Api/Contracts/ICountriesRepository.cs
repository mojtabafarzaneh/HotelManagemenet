using Hotelmanagment.Api.Data;

namespace Hotelmanagment.Api.Contracts;

public interface ICountriesRepository: IGenericRepository<Country>
{
    public Task<Country> GetCountryAsync(int id);
    
}