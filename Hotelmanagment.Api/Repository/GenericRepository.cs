using Hotelmanagment.Api.Contracts;
using Hotelmanagment.Api.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Hotelmanagment.Api.Repository;

public class GenericRepository<T>: IGenericRepository<T> where T : class
{
    private readonly HotelManagmentDBContext _dbcontext;
    public GenericRepository(HotelManagmentDBContext dbContext)
    {
        _dbcontext = dbContext;
        
    }
    public async Task<T> GetAsync(int? id)
    {
        if (id is null)
        {
            return null;
        }
        
        return await _dbcontext.Set<T>().FindAsync(id);
        
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbcontext.Set<T>().ToListAsync();
        
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbcontext.AddAsync(entity);
        await _dbcontext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbcontext.Update(entity);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        _dbcontext.Set<T>().Remove(entity);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }
}