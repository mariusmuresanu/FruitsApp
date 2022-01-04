﻿using System;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fruit>>> GetFruits()
        {
            return await _context.Fruits.ToListAsync();
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
