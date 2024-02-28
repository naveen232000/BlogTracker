using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogTracker.Data;
using BlogTracker.Models;

namespace BlogTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminInfoesApiController : ControllerBase
    {
        private readonly BlogTrackerdbContext _context;

        public AdminInfoesApiController(BlogTrackerdbContext context)
        {
            _context = context;
        }

        // GET: api/AdminInfoesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminInfo>>> GetAdminInfo()
        {
          if (_context.AdminInfo == null)
          {
              return NotFound();
          }
            return await _context.AdminInfo.ToListAsync();
        }

        // GET: api/AdminInfoesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminInfo>> GetAdminInfo(string id)
        {
          if (_context.AdminInfo == null)
          {
              return NotFound();
          }
            var adminInfo = await _context.AdminInfo.FindAsync(id);

            if (adminInfo == null)
            {
                return NotFound();
            }

            return adminInfo;
        }

        // PUT: api/AdminInfoesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminInfo(string id, AdminInfo adminInfo)
        {
            if (id != adminInfo.EmailId)
            {
                return BadRequest();
            }

            _context.Entry(adminInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminInfoExists(id))
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

        // POST: api/AdminInfoesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminInfo>> PostAdminInfo(AdminInfo adminInfo)
        {
          if (_context.AdminInfo == null)
          {
              return Problem("Entity set 'BlogTrackerdbContext.AdminInfo'  is null.");
          }
            _context.AdminInfo.Add(adminInfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdminInfoExists(adminInfo.EmailId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdminInfo", new { id = adminInfo.EmailId }, adminInfo);
        }

        // DELETE: api/AdminInfoesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminInfo(string id)
        {
            if (_context.AdminInfo == null)
            {
                return NotFound();
            }
            var adminInfo = await _context.AdminInfo.FindAsync(id);
            if (adminInfo == null)
            {
                return NotFound();
            }

            _context.AdminInfo.Remove(adminInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminInfoExists(string id)
        {
            return (_context.AdminInfo?.Any(e => e.EmailId == id)).GetValueOrDefault();
        }
    }
}
