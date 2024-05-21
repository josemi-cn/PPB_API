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
    public class PermissionsRolesController : ControllerBase
    {
        private readonly PpbStorageContext _context;

        public PermissionsRolesController(PpbStorageContext context)
        {
            _context = context;
        }

        // GET: api/PermissionsRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionsRole>>> GetPermissionsRoles()
        {
          if (_context.PermissionsRoles == null)
          {
              return NotFound();
          }
            return await _context.PermissionsRoles.ToListAsync();
        }

        // GET: api/PermissionsRoles
        [HttpGet("Role/{role_id}/Permission/{permission_id}")]
        public async Task<ActionResult<int>> GetPermissionRolId(int role_id, int permission_id)
        {
            if (_context.PermissionsRoles == null)
            {
                return NotFound();
            }
            return await _context.PermissionsRoles.Where(a => a.RoleId == role_id && a.PermissionId == permission_id).Select(a => a.Id).FirstAsync();
        }

        // GET: api/PermissionsRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionsRole>> GetPermissionsRole(int id)
        {
          if (_context.PermissionsRoles == null)
          {
              return NotFound();
          }
            var permissionsRole = await _context.PermissionsRoles.FindAsync(id);

            if (permissionsRole == null)
            {
                return NotFound();
            }

            return permissionsRole;
        }

        // PUT: api/PermissionsRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermissionsRole(int id, PermissionsRole permissionsRole)
        {
            if (id != permissionsRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(permissionsRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionsRoleExists(id))
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

        // POST: api/PermissionsRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PermissionsRole>> PostPermissionsRole(PermissionsRole permissionsRole)
        {
          if (_context.PermissionsRoles == null)
          {
              return Problem("Entity set 'PpbStorageContext.PermissionsRoles'  is null.");
          }
            _context.PermissionsRoles.Add(permissionsRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermissionsRole", new { id = permissionsRole.Id }, permissionsRole);
        }

        // DELETE: api/PermissionsRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermissionsRole(int id)
        {
            if (_context.PermissionsRoles == null)
            {
                return NotFound();
            }
            var permissionsRole = await _context.PermissionsRoles.FindAsync(id);
            if (permissionsRole == null)
            {
                return NotFound();
            }

            _context.PermissionsRoles.Remove(permissionsRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/PermissionsRoles/Role/5
        [HttpDelete("Role/{role_id}/Permission/{permission_id}")]
        public async Task<IActionResult> DeletePermissionsRole(int role_id, int permission_id)
        {
            if (_context.PermissionsRoles == null)
            {
                return NotFound();
            }
            var permissionsRole = _context.PermissionsRoles.Where(a => a.RoleId == role_id && a.PermissionId == permission_id).First();
            if (permissionsRole == null)
            {
                return NotFound();
            }

            _context.PermissionsRoles.Remove(permissionsRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermissionsRoleExists(int id)
        {
            return (_context.PermissionsRoles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
