using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FruitsApp.Models;

namespace FruitsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsController : ControllerBase
    {
        private readonly FruitsDbContext _context;

        public FruitsController(FruitsDbContext context)
        {
            _context = context;
        }

        // GET: api/Fruits
        /// <summary>
        /// Get a list of all the fruits
        /// </summary>
        /// <param name="from">Filter fruits added from this date time (inclusive). Leave empty for no lower limit.</param>
        /// <param name="to">Filter fruits add up to this date time (inclusive). Leave empty for no upper limit.</param>
        /// <returns>A list of fruit objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fruit>>> GetFruits(
            [FromQuery]DateTimeOffset? from = null, 
            [FromQuery] DateTimeOffset? to = null)
        {
            IQueryable<Fruit> result = _context.Fruits;

            if (from != null)
            {
                 result = result.Where(f => from <= f.DateAdded);
            }
            if (to != null)
            {
                result = result.Where(f => f.DateAdded <= to);
            }

            //if (from != null && to != null)
            //{
            //    result = result.Where(f => from <= f.DateAdded && f.DateAdded <= to);
            //}
            //else if (from != null)
            //{
            //    result = result.Where(f => from <= f.DateAdded);
            //}
            //else if (to != null)
            //{
            //    result = result.Where(f => f.DateAdded <= to);
            //}


            var resultList = await result
                .OrderByDescending(f => f.MarketPrice)
                .ToListAsync();
            return resultList;
        }

        // GET: api/Fruits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fruit>> GetFruit(long id)
        {
            var fruit = await _context.Fruits.FindAsync(id);

            if (fruit == null)
            {
                return NotFound();
            }

            return fruit;
        }

        // PUT: api/Fruits/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFruit(long id, Fruit fruit)
        {
            if (id != fruit.Id)
            {
                return BadRequest();
            }

            _context.Entry(fruit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FruitExists(id))
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

        //TODO make CRUD comments work with URL api/Fruits/{id}/Comments
        //TODO make CRUD with another comments controller: api/comments/{Flower id}
        //TODO write a validator for comments

        // POST: api/Fruits
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Fruit>> PostFruit(Fruit fruit)
        {
            _context.Fruits.Add(fruit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFruit", new { id = fruit.Id }, fruit);
        }

        // DELETE: api/Fruits/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fruit>> DeleteFruit(long id)
        {
            var fruit = await _context.Fruits.FindAsync(id);
            if (fruit == null)
            {
                return NotFound();
            }

            _context.Fruits.Remove(fruit);
            await _context.SaveChangesAsync();

            return fruit;
        }

        private bool FruitExists(long id)
        {
            return _context.Fruits.Any(e => e.Id == id);
        }
    }
}
