using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPB_Storage_API.Models;

namespace PPB_Storage_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsCommandsController : ControllerBase
    {
        private readonly PpbStorageContext _context;

        public ProductsCommandsController(PpbStorageContext context)
        {
            _context = context;
        }

        // GET: api/ProductsCommands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsCommand>>> GetProductsCommands()
        {
          if (_context.ProductsCommands == null)
          {
              return NotFound();
          }
            return await _context.ProductsCommands.ToListAsync();
        }

        // GET: api/ProductsCommands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsCommand>> GetProductsCommand(int id)
        {
          if (_context.ProductsCommands == null)
          {
              return NotFound();
          }
            var productsCommand = await _context.ProductsCommands.FindAsync(id);

            if (productsCommand == null)
            {
                return NotFound();
            }

            return productsCommand;
        }

        // GET: api/ProductsCommands/Command/5
        [HttpGet("Command/{id}")]
        public async Task<ActionResult<List<ProductsCommand>>> GetCommand(int id)
        {
            if (_context.ProductsCommands == null)
            {
                return NotFound();
            }
            var productsCommand = _context.ProductsCommands.Where(a => a.CommandId == id).ToList();

            if (productsCommand == null)
            {
                return NotFound();
            }

            return productsCommand;
        }

        // PUT: api/ProductsCommands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductsCommand(int id, ProductsCommand productsCommand)
        {
            if (id != productsCommand.Id)
            {
                return BadRequest();
            }

            _context.Entry(productsCommand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsCommandExists(id))
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

        // POST: api/ProductsCommands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductsCommand>> PostProductsCommand(ProductsCommand productsCommand)
        {
          if (_context.ProductsCommands == null)
          {
              return Problem("Entity set 'PpbStorageContext.ProductsCommands'  is null.");
          }
            _context.ProductsCommands.Add(productsCommand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductsCommand", new { id = productsCommand.Id }, productsCommand);
        }

        // DELETE: api/ProductsCommands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductsCommand(int id)
        {
            if (_context.ProductsCommands == null)
            {
                return NotFound();
            }
            var productsCommand = await _context.ProductsCommands.FindAsync(id);
            if (productsCommand == null)
            {
                return NotFound();
            }

            _context.ProductsCommands.Remove(productsCommand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsCommandExists(int id)
        {
            return (_context.ProductsCommands?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
