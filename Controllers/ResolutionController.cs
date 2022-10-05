using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResolutionManagement.Data;
using ResolutionManagement.Models;

namespace ResolutionManagement.Controllers
{
    public class ResolutionController : Controller
    {
        private ApplicationDbContext _context;
        private readonly ILogger<ResolutionController> _logger;
        public ResolutionController(ILogger<ResolutionController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Resolution
        public async Task<IActionResult> Index()
        {
              return _context.Resolutions != null ? 
                          View(await _context.Resolutions.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Resolutions'  is null.");
        }

        // GET: Resolution/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resolutions == null)
            {
                return NotFound();
            }

            var resolution = await _context.Resolutions
                .FirstOrDefaultAsync(m => m.ResolutionId == id);
            if (resolution == null)
            {
                return NotFound();
            }

            return View(resolution);
        }

        // GET: Resolution/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resolution/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResolutionId,CreationDate,Abstract,Status,OwnerUserID")] Resolution resolution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resolution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResolutionId"] = new SelectList(_context.Resolutions, "ResolutionId", "Abstract", resolution.ResolutionId);
            return View(resolution);
        }

        // GET: Resolution/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resolutions == null)
            {
                return NotFound();
            }

            var resolution = await _context.Resolutions.FindAsync(id);
            if (resolution == null)
            {
                return NotFound();
            }
            ViewData["ResolutionId"] = new SelectList(_context.Resolutions, "ResolutionId", "Abstract", resolution.ResolutionId);
            return View(resolution);
        }

        // POST: Resolution/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ResolutionId,CreationDate,Abstract,Status,OwnerUserID")] Resolution resolution)
        {
            if (id != resolution.ResolutionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resolution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResolutionExists(resolution.ResolutionId))
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
            return View(resolution);
        }

        // GET: Resolution/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resolutions == null)
            {
                return NotFound();
            }

            var resolution = await _context.Resolutions
                .FirstOrDefaultAsync(m => m.ResolutionId == id);
            if (resolution == null)
            {
                return NotFound();
            }

            return View(resolution);
        }

        // POST: Resolution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Resolutions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Resolutions'  is null.");
            }
            var resolution = await _context.Resolutions.FindAsync(id);
            if (resolution != null)
            {
                _context.Resolutions.Remove(resolution);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResolutionExists(int? id)
        {
          return (_context.Resolutions?.Any(e => e.ResolutionId == id)).GetValueOrDefault();
        }
    }
}
