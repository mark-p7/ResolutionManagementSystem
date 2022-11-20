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
    public class FeedbackRequestsController : Controller
    {
        private ApplicationDbContext _context;
        private readonly ILogger<ResolutionController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        public FeedbackRequestsController(ILogger<ResolutionController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        // GET: FeedbackRequests
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(User);
            List<FeedbackRequest> feedbacks = await (from FeedbackRequest in _context.FeedbackRequests select FeedbackRequest).ToListAsync();
            foreach (var item in feedbacks)
            {
                Console.Write(item.OwnerUserID + "\n");
            }
            Console.Write("USER ID:  " + id + "\n\n");
            List<FeedbackRequest> userFeedbacks = feedbacks.FindAll(feedback => feedback.OwnerUserID == id);

            return _context.Resolutions != null ?
                    View(userFeedbacks) :
                    Problem("Entity set 'ApplicationDbContext.FeedbackRequests'  is null.");
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
            Console.Write("HIT EDIT ----- ID: " + id + "\n");
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
                    feedbackRequest.Resolved = true;
                    _context.Update(feedbackRequest);
                    await _context.SaveChangesAsync();
                    int? ResolutionId = feedbackRequest.ResolutionId;
                    Resolution[] resolutions = _context.Resolutions.ToArray();
                    Resolution resolutionInQuestion = new Resolution();
                    foreach (Resolution resolution in resolutions)
                    {
                        if (resolution.ResolutionId == ResolutionId)
                        {
                            resolutionInQuestion = resolution;
                        }
                    }
                    FeedbackRequest[] feedbackRequestsUnFiltered = _context.FeedbackRequests.ToArray();
                    FeedbackRequest[] feedbackRequests = (from FeedbackRequest in feedbackRequestsUnFiltered where FeedbackRequest.ResolutionId == ResolutionId select FeedbackRequest).ToArray();
                    double rejected = 0;
                    double accepted = 0;
                    double count = 0;
                    foreach (FeedbackRequest feedback in feedbackRequests)
                    {
                        count++;
                        if (feedback.Resolved == true){
                            if (feedback.Accepted == true)
                            {
                                accepted++;
                            }
                            else
                            {
                                rejected++;
                            }
                        }
                    }

                    if (accepted / count > 0.5)
                    {
                        resolutionInQuestion.Status = "Accepted";
                    }
                    else if (rejected / count > 0.5)
                    {
                        resolutionInQuestion.Status = "Rejected";
                    }
                    else
                    {
                        resolutionInQuestion.Status = "In Progress";
                    }   
                    _context.Update(resolutionInQuestion);
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
