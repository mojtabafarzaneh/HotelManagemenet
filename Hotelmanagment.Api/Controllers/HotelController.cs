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

namespace Hotelmanagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelsRepository _hotelRepo;
        private readonly IMapper _mapper;
        public HotelController(IHotelsRepository hotelRepo, IMapper mapper)
        {
            _mapper = mapper;
            _hotelRepo = hotelRepo;
        }

        // GET: api/Hotel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelDTO>>> GetHotels()
        {
            var hotels =await _hotelRepo.GetAllAsync();
            var result = _mapper.Map<List<GetHotelDTO>>(hotels);
            return Ok(result);
        }

        // GET: api/Hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDTO>> GetHotel(int id)
        {
            var hotel = await _hotelRepo.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }
            
            var result = _mapper.Map<GetHotelDTO>(hotel);

            return result;
        }

        // PUT: api/Hotel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, GetHotelDTO hoteldto)
        {
            if (id != hoteldto.Id)
            {
                return BadRequest();
            }
            
            var hotel = _mapper.Map<Hotel>(hoteldto);

            try
            {
                await _hotelRepo.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
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

        // POST: api/Hotel
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDTO hoteldto)
        {
            var hotel = _mapper.Map<Hotel>(hoteldto);
            await _hotelRepo.AddHotelAsync(hotel);

            if (hotel.Country == null)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepo.Exists(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelRepo.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepo.Exists(id);
        }
    }
}
