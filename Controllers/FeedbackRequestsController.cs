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
    public class FeedbackRequestsController : Controller
    {
        private ApplicationDbContext _context;
        private readonly ILogger<ResolutionController> _logger;

        public FeedbackRequestsController(ILogger<ResolutionController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: FeedbackRequests
        public async Task<IActionResult> Index()
        {
            return _context.Resolutions != null ? 
                    View(await _context.FeedbackRequests.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.FeedbackRequests'  is null.");
            // var applicationDbContext = _context.FeedbackRequests.Include(f => f.Resolution);
            // return View(await applicationDbContext.ToListAsync());
        }

        // GET: FeedbackRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FeedbackRequests == null)
            {
                return NotFound();
            }

            var feedbackRequest = await _context.FeedbackRequests
                .Include(f => f.Resolution)
                .FirstOrDefaultAsync(m => m.FeedbackRequestId == id);
            if (feedbackRequest == null)
            {
                return NotFound();
            }

            return View(feedbackRequest);
        }

        // GET: FeedbackRequests/Create
        public IActionResult Create()
        {
            ViewData["ResolutionId"] = new SelectList(_context.Resolutions, "ResolutionId", "ResolutionId");
            return View();
        }

        // POST: FeedbackRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeedbackRequestId,CreationDate,Description,ESignature,OwnerUserID,Resolved,Accepted,ResolutionId")] FeedbackRequest feedbackRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedbackRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResolutionId"] = new SelectList(_context.Resolutions, "ResolutionId", "ResolutionId", feedbackRequest.ResolutionId);
            return View(feedbackRequest);
        }

        // GET: FeedbackRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FeedbackRequests == null)
            {
                return NotFound();
            }

            var feedbackRequest = await _context.FeedbackRequests.FindAsync(id);
            if (feedbackRequest == null)
            {
                return NotFound();
            }
            ViewData["ResolutionId"] = new SelectList(_context.Resolutions, "ResolutionId", "ResolutionId", feedbackRequest.ResolutionId);
            return View(feedbackRequest);
        }

        // POST: FeedbackRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("FeedbackRequestId,CreationDate,Description,ESignature,OwnerUserID,Resolved,Accepted,ResolutionId")] FeedbackRequest feedbackRequest)
        {
            if (id != feedbackRequest.FeedbackRequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedbackRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackRequestExists(feedbackRequest.FeedbackRequestId))
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
            ViewData["ResolutionId"] = new SelectList(_context.Resolutions, "ResolutionId", "ResolutionId", feedbackRequest.ResolutionId);
            return View(feedbackRequest);
        }

        // GET: FeedbackRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FeedbackRequests == null)
            {
                return NotFound();
            }

            var feedbackRequest = await _context.FeedbackRequests
                .Include(f => f.Resolution)
                .FirstOrDefaultAsync(m => m.FeedbackRequestId == id);
            if (feedbackRequest == null)
            {
                return NotFound();
            }

            return View(feedbackRequest);
        }

        // POST: FeedbackRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.FeedbackRequests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FeedbackRequests'  is null.");
            }
            var feedbackRequest = await _context.FeedbackRequests.FindAsync(id);
            if (feedbackRequest != null)
            {
                _context.FeedbackRequests.Remove(feedbackRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackRequestExists(int? id)
        {
          return (_context.FeedbackRequests?.Any(e => e.FeedbackRequestId == id)).GetValueOrDefault();
        }
    }
}
