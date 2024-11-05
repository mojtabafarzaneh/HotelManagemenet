using Microsoft.EntityFrameworkCore;

namespace Hotelmanagment.Api.Data;

public class HotelManagmentDBContext: DbContext
{
    public HotelManagmentDBContext(DbContextOptions<HotelManagmentDBContext> options) : base(options)
    {
        
    }
    
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country?> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Country>().HasData(
        new Country
        {
            Id = 1,
            Name = "United States",
            
        },
        new Country
        {
            Id = 2,
            Name = "Canada",
        },
        new Country
        {
            Id = 3,
            Name = "Iran",
        });
        modelBuilder.Entity<Hotel>().HasData(
        new Hotel
        {
            Id = 1,
            Name = "Grand Hotel",
            Address = "Rasht, Golsar",
            CountryId = 3,
            Rating = "3.6"
        },
        new Hotel
        {
            Id = 2,
            Name = "Hilton",
            Address = "L.A, California",
            CountryId = 1,
            Rating = "5.0"
        });

    }
    
    
}