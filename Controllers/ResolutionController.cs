using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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

        private readonly UserManager<IdentityUser> _userManager;
        public ResolutionController(ILogger<ResolutionController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: Resolution
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(User);
            // Finds Resolved Feedbacks
            FeedbackRequest[] myFeedBackRequests = _context.FeedbackRequests.ToArray();
            FeedbackRequest[] ResolvedFeedbacks = Array.FindAll(myFeedBackRequests, feedback => feedback.OwnerUserID == id && feedback.Resolved == false);
            // Getting Array of Resolutions that are already resolved
            Resolution[] resolutions = _context.Resolutions.ToArray();
            Resolution[] filteredResolutionsByResolved = new Resolution[] { };
            foreach (var resolution in resolutions)
            {
                foreach (var resolvedFeedback in ResolvedFeedbacks)
                {
                    if (resolution.ResolutionId == resolvedFeedback.ResolutionId && resolvedFeedback.Resolved == false)
                    {
                        int index = Array.IndexOf(filteredResolutionsByResolved, null);
                        if (index != -1)
                        {
                            filteredResolutionsByResolved[index] = resolution;
                        }
                        else
                        {
                            Array.Resize(ref filteredResolutionsByResolved, filteredResolutionsByResolved.Length + 1);
                            filteredResolutionsByResolved[filteredResolutionsByResolved.GetUpperBound(0)] = resolution;
                        }
                    }
                }
            }
            Resolution[] filteredResolutionsByResolvedNoDuplicates = filteredResolutionsByResolved.Distinct().ToArray();
            ViewData["ResolutionsAlreadyResolved"] = filteredResolutionsByResolvedNoDuplicates;
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

        // GET: Resolution/Resolve/5
        public async Task<IActionResult> Resolve(int? id)
        {
            Console.Write("Hit Resolve + " + id + "\n\n\\n");
            if (id == null || _context.Resolutions == null)
            {
                return NotFound();
            }
            FeedbackRequest[] feedbackRequests = (from FeedbackRequest in _context.FeedbackRequests select FeedbackRequest).ToArray();
            foreach (FeedbackRequest feedbackRequest in feedbackRequests)
            {
                Console.Write(feedbackRequest.ResolutionId + "\n");
                if (feedbackRequest.ResolutionId == id)
                {
                    Console.Write("Id found: " + feedbackRequest.ResolutionId + "\n\n");
                    return RedirectToAction("Edit", "FeedbackRequests", new { id = feedbackRequest.FeedbackRequestId });
                }
            }
            return NotFound();
            // var feedback = await _context.FeedbackRequests.FindAsync();
            // if (feedback == null)
            // {
            //     return NotFound();
            // }
            // ViewData["ResolutionId"] = new SelectList(_context.Resolutions, "ResolutionId", "Abstract", resolution.ResolutionId);
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
