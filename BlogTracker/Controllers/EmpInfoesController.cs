using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogTracker.Data;
using BlogTracker.Models;

namespace BlogTracker.Controllers
{
    public class EmpInfoesController : Controller
    {
        private readonly BlogTrackerdbContext _context;

        public EmpInfoesController(BlogTrackerdbContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind("EmailId,PassCode")] EmpInfo empInfo)
        {
            var emp = _context.EmpInfo.SingleOrDefault(a => a.EmailId == empInfo.EmailId && a.PassCode == empInfo.PassCode);

            if (emp != null)
            {
                TempData["EmpEmail"] = empInfo.EmailId;
                HttpContext.Session.SetString("EmployeeEmail", emp.EmailId);
                return RedirectToAction("EmpBlogIndex", "BlogInfoes");
            }
            else
            {
                // Authentication failed
                return View();
            }
        }
        //based on emailid
      
        // GET: EmpInfoes
        public async Task<IActionResult> Index()
        {
              return _context.EmpInfo != null ? 
                          View(await _context.EmpInfo.ToListAsync()) :
                          Problem("Entity set 'BlogTrackerdbContext.EmpInfo'  is null.");
        }

        // GET: EmpInfoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.EmpInfo == null)
            {
                return NotFound();
            }

            var empInfo = await _context.EmpInfo
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (empInfo == null)
            {
                return NotFound();
            }

            return View(empInfo);
        }

        // GET: EmpInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmailId,Name,DateOfJoining,PassCode")] EmpInfo empInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empInfo);
        }

        // GET: EmpInfoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.EmpInfo == null)
            {
                return NotFound();
            }

            var empInfo = await _context.EmpInfo.FindAsync(id);
            if (empInfo == null)
            {
                return NotFound();
            }
            return View(empInfo);
        }

        // POST: EmpInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmailId,Name,DateOfJoining,PassCode")] EmpInfo empInfo)
        {
            if (id != empInfo.EmailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpInfoExists(empInfo.EmailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(empInfo);
        }

        // GET: EmpInfoes/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.EmpInfo == null)
            {
                return NotFound();
            }

            var empInfo = await _context.EmpInfo
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (empInfo == null)
            {
                return NotFound();
            }

            return View(empInfo);
        }

        // POST: EmpInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.EmpInfo == null)
            {
                return Problem("Entity set 'BlogTrackerdbContext.EmpInfo'  is null.");
            }
            var empInfo = await _context.EmpInfo.FindAsync(id);
            if (empInfo != null)
            {
                _context.EmpInfo.Remove(empInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpInfoExists(string id)
        {
          return (_context.EmpInfo?.Any(e => e.EmailId == id)).GetValueOrDefault();
        }
    }
}
