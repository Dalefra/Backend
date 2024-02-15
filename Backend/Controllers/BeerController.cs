using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _storeContext;

        public BeerController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get()
        {
            return await _storeContext.Beers.Select(x => new BeerDto
            {
                Id = x.BeerID,
                Name = x.Name,
                Alcohol = x.Alcohol,
                BrandId = x.BrandID,
            }).ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetId(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandID,
            };

            return Ok(beerDto);

        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var berr = new Beer()
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandId,
                Alcohol = beerInsertDto.Alcohol
            };

            await _storeContext.AddAsync(berr);
            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = berr.BeerID, Name = berr.Name, Alcohol = berr.Alcohol, BrandId = berr.BrandID,
            };

            return CreatedAtAction(nameof(GetId), new { id = berr.BeerID }, beerDto);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            beer.Name = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;
            beer.BrandID = beerUpdateDto.BrandId;

            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandId = beer.BrandID,
            };

            return Ok( beerDto );

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            _storeContext.Beers.Remove(beer);
            await _storeContext.SaveChangesAsync();
     
            return NoContent();
        }
    }
}
