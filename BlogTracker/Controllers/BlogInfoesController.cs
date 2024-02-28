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
    public class BlogInfoesController : Controller
    {
        private readonly BlogTrackerdbContext _context;

        public BlogInfoesController(BlogTrackerdbContext context)
        {
            _context = context;
        }
        //Get Blog based on empemail
        public async Task<IActionResult> EmpBlogIndex()
        {
            if (TempData["EmpEmail"] == null || _context.BlogInfo == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo
                .Where(m => m.EmpEmailId == TempData["EmpEmail"].ToString()).ToListAsync();
            if (blogInfo == null)
            {
                return NotFound();
            }

            return View(blogInfo);
        }

        // GET: BlogInfoes
        public async Task<IActionResult> Index()
        {
              return _context.BlogInfo != null ? 
                          View(await _context.BlogInfo.ToListAsync()) :
                          Problem("Entity set 'BlogTrackerdbContext.BlogInfo'  is null.");
        }

        // GET: BlogInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BlogInfo == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blogInfo == null)
            {
                return NotFound();
            }

            return View(blogInfo);
        }

        // GET: BlogInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Title,Subject,DateOfCreation,BlogUrl,EmpEmailId")] BlogInfo blogInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogInfo);
                await _context.SaveChangesAsync();
                TempData["EmpEmail"] = blogInfo.EmpEmailId;
                return RedirectToAction("EmpBlogIndex");
            }
            return View(blogInfo);
        }

        // GET: BlogInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BlogInfo == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo.FindAsync(id);
            if (blogInfo == null)
            {
                return NotFound();
            }
            return View(blogInfo);
        }

        // POST: BlogInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,Title,Subject,DateOfCreation,BlogUrl,EmpEmailId")] BlogInfo blogInfo)
        {
            if (id != blogInfo.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogInfoExists(blogInfo.BlogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["EmpEmail"] = blogInfo.EmpEmailId;
                return RedirectToAction("EmpBlogIndex");
            }
            return View(blogInfo);
        }

        // GET: BlogInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BlogInfo == null)
            {
                return NotFound();
            }

            var blogInfo = await _context.BlogInfo
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blogInfo == null)
            {
                return NotFound();
            }

            return View(blogInfo);
        }

        // POST: BlogInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BlogInfo == null)
            {
                return Problem("Entity set 'BlogTrackerdbContext.BlogInfo'  is null.");
            }
            var blogInfo = await _context.BlogInfo.FindAsync(id);
            TempData["EmpEmail"] = blogInfo.EmpEmailId;
            if (blogInfo != null)
            {
                _context.BlogInfo.Remove(blogInfo);
            }
            
            await _context.SaveChangesAsync();
          
            return RedirectToAction("EmpBlogIndex");
        }

        private bool BlogInfoExists(int id)
        {
          return (_context.BlogInfo?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
    }
}
