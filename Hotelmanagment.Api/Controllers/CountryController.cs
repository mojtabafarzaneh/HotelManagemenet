using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotelmanagment.Api.Data;
using Hotelmanagment.Api.DTO;

namespace Hotelmanagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly HotelManagmentDBContext _dbcontext;
        private readonly IMapper _mapper;

        public CountryController(HotelManagmentDBContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        // GET: api/Country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
        {
            var countries =await _dbcontext.Countries.Include(q=>q.Hotels).ToListAsync();
            var records = _mapper.Map<List<GetCountryDTO>>(countries);
            return Ok(records);
        }

        // GET: api/Country/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDTO>> GetCountry(int id)
        {
            var country = await _dbcontext.Countries.Include(q=> q.Hotels)
                .FirstOrDefaultAsync(q=> q.Id == id);

            if (country == null)
            {
                return NotFound();
            }
            
            var result = _mapper.Map<GetCountryDTO>(country);
            

            return result;
        }

        // PUT: api/Country/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO update)
        {
            if (id != update.Id)
            {
                return BadRequest();
            }

            var country = await _dbcontext.Countries.FindAsync(update.Id);
            if (country == null)
            {
                return NotFound();
            }
            _mapper.Map(update, country);

            try
            {
                await _dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Country
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CountryDTO Createcountry)
        {
            var create = _mapper.Map<Country>(Createcountry);
            _dbcontext.Countries.Add(create);
            await _dbcontext.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = create.Id }, create);
        }

        // DELETE: api/Country/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _dbcontext.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _dbcontext.Countries.Remove(country);
            await _dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _dbcontext.Countries.Any(e => e.Id == id);
        }
    }
}
