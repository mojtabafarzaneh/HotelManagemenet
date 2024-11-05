using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hotelmanagment.Api.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotelmanagment.Api.Data;
using Hotelmanagment.Api.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Hotelmanagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRep;

        public CountryController(IMapper mapper, ICountriesRepository countriesRep)
        {
            _countriesRep = countriesRep;
            _mapper = mapper;
        }

        // GET: api/Country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
        {
            var countries = await _countriesRep.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDTO>>(countries);
            return Ok(records);
        }

        // GET: api/Country/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDTO>> GetCountry(int id)
        {
            var country = await _countriesRep.GetCountryAsync(id);

            if (country == null)
            {
                return NotFound();
            }
            
            var result = _mapper.Map<GetCountryDTO>(country);
            

            return result;
        }

        // PUT: api/Country/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO update)
        {
            if (id != update.Id)
            {
                return BadRequest();
            }

            var country = await _countriesRep.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            _mapper.Map(update, country);

            try
            {
                await _countriesRep.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
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
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CountryDTO Createcountry)
        {
            var create = _mapper.Map<Country>(Createcountry);
            await _countriesRep.AddAsync(create);

            return CreatedAtAction("GetCountry", new { id = create.Id }, create);
        }

        // DELETE: api/Country/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRep.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

           
            await _countriesRep.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRep.Exists(id);
        }
    }
}
