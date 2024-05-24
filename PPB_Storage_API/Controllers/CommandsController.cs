using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPB_Storage_API.Models;

namespace PPB_Storage_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly PpbStorageContext _context;

        public CommandsController(PpbStorageContext context)
        {
            _context = context;
        }

        // GET: api/Commands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Command>>> GetCommands()
        {
          if (_context.Commands == null)
          {
              return NotFound();
          }
            return await _context.Commands.ToListAsync();
        }

        // GET: api/Commands
        [HttpGet("Filter/")]
        public async Task<ActionResult<IEnumerable<Command>>> GetCommandsFiltered()
        {
            if (_context.Commands == null)
            {
                return NotFound();
            }
            DateTime FHOD = DateTime.Today;
            DateTime LHOD = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
            return await _context.Commands.Where(a => a.Date >= FHOD && a.Date <= LHOD && a.Delivered == false).OrderBy(a => a.Number).ToListAsync();
        }

        // GET: api/Commands
        [HttpGet("Filter/Id/List")]
        public async Task<ActionResult<string>> GetCommandsIdListedAndFiltered()
        {
            if (_context.Commands == null)
            {
                return NotFound();
            }
            DateTime FHOD = DateTime.Today;
            DateTime LHOD = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);
            string text = "{";

            List<Command> list = await _context.Commands.Where(a => a.Date >= FHOD && a.Date <= LHOD && a.Delivered == false).OrderBy(a => a.Number).ToListAsync();

            list.ForEach(delegate (Command c)
            {
                text += c.Id + ", ";
            });

            text += "}";

            return text;
        }

        // GET: api/Commands
        [HttpGet("Filter/Last")]
        public async Task<ActionResult<Command>> GetLastFilteredCommand()
        {
            Command cNull = new Command();

            cNull.Id = 0;
            cNull.Number = 0;
            cNull.Date = DateTime.Today;
            cNull.Ready = true;
            cNull.Delivered = true;

            if (_context.Commands == null)
            {
                return cNull;
            }
            DateTime FHOD = DateTime.Today;
            DateTime LHOD = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);

            int c = _context.Commands.Where(a => a.Date >= FHOD && a.Date <= LHOD).Count();

            if (c == 0)
            {
                return cNull;
            }

            return _context.Commands.Where(a => a.Date >= FHOD && a.Date <= LHOD).OrderByDescending(a => a.Number).First();
        }

        // GET: api/Commands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Command>> GetCommand(int id)
        {
          if (_context.Commands == null)
          {
              return NotFound();
          }
            var command = await _context.Commands.FindAsync(id);

            if (command == null)
            {
                return NotFound();
            }

            return command;
        }

        // PUT: api/Commands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommand(int id, Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            _context.Entry(command).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandExists(id))
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

        // POST: api/Commands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Command>> PostCommand(Command command)
        {
            if (_context.Commands == null)
            {
                return Problem("Entity set 'PpbStorageContext.Commands'  is null.");
            }

            command.Date = DateTime.Now;

            _context.Commands.Add(command);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommand", new { id = command.Id }, command);
        }

        // DELETE: api/Commands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommand(int id)
        {
            if (_context.Commands == null)
            {
                return NotFound();
            }
            var command = await _context.Commands.FindAsync(id);
            if (command == null)
            {
                return NotFound();
            }

            _context.Commands.Remove(command);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommandExists(int id)
        {
            return (_context.Commands?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
