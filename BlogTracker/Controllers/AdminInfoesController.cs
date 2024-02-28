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
    public class AdminInfoesController : Controller
    {
        private readonly BlogTrackerdbContext _context;

        public AdminInfoesController(BlogTrackerdbContext context)
        {
            _context = context;
        }
        //adminlogin

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind("EmailId,Password")] AdminInfo adminInfo)
        {
            var admin = _context.AdminInfo.SingleOrDefault(a => a.EmailId == adminInfo.EmailId && a.Password == adminInfo.Password);

            if (admin != null)
            {
                // Admin authenticated successfully, set up your session or cookie here
                HttpContext.Session.SetString("AdminEmail", admin.EmailId);
                return RedirectToAction("Index","EmpInfoes");
            }
            else
            {
                // Authentication failed
                return View();
            }
        }

        //// GET: AdminInfoes
        //public async Task<IActionResult> Index()
        //{
        //      return _context.AdminInfo != null ? 
        //                  View(await _context.AdminInfo.ToListAsync()) :
        //                  Problem("Entity set 'BlogTrackerdbContext.AdminInfo'  is null.");
        //}

        //// GET: AdminInfoes/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null || _context.AdminInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    var adminInfo = await _context.AdminInfo
        //        .FirstOrDefaultAsync(m => m.EmailId == id);
        //    if (adminInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(adminInfo);
        //}

        // GET: AdminInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmailId,Password")] AdminInfo adminInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminInfo);
        }

        //// GET: AdminInfoes/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null || _context.AdminInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    var adminInfo = await _context.AdminInfo.FindAsync(id);
        //    if (adminInfo == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(adminInfo);
        //}

        //// POST: AdminInfoes/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("EmailId,Password")] AdminInfo adminInfo)
        //{
        //    if (id != adminInfo.EmailId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(adminInfo);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AdminInfoExists(adminInfo.EmailId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(adminInfo);
        //}

        //// GET: AdminInfoes/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null || _context.AdminInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    var adminInfo = await _context.AdminInfo
        //        .FirstOrDefaultAsync(m => m.EmailId == id);
        //    if (adminInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(adminInfo);
        //}

        //// POST: AdminInfoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    if (_context.AdminInfo == null)
        //    {
        //        return Problem("Entity set 'BlogTrackerdbContext.AdminInfo'  is null.");
        //    }
        //    var adminInfo = await _context.AdminInfo.FindAsync(id);
        //    if (adminInfo != null)
        //    {
        //        _context.AdminInfo.Remove(adminInfo);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool AdminInfoExists(string id)
        //{
        //  return (_context.AdminInfo?.Any(e => e.EmailId == id)).GetValueOrDefault();
        //}
    }
}
