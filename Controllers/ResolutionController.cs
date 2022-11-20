using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        // [Route("Resolution/{id}")]
        public async Task<IActionResult> Index(string resolutionStatus, string searchString)
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
            ViewData["BoardMembers"] = _userManager.GetUsersInRoleAsync("Member").Result.ToArray();
            ViewData["OwnerUserID"] = id;

            //Filter by status
            var StatusList = new List<string>();

            var StatusQry = from s in _context.Resolutions
                            orderby s.Status
                            select s.Status;

            StatusList.AddRange(StatusQry.Distinct());
            ViewBag.resolutionStatus = new SelectList(StatusList);

            //added search functionality
            // string searchString = id;
            var resolutionsSearchResult = from m in _context.Resolutions select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                resolutionsSearchResult = resolutionsSearchResult.Where(s => s.Abstract!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(resolutionStatus))
            {
                resolutionsSearchResult = resolutionsSearchResult.Where(x => x.Status == resolutionStatus);
            }

            return _context.Resolutions != null ?
                        // View(await _context.Resolutions.ToListAsync()) :
                        View(await resolutionsSearchResult.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Resolutions'  is null.");
        }

        [HttpPost]
        public string Index(FormCollection fc, string searchString)
        {
            return "<h3> From [HttpPost]Index: " + searchString + "</h3>";
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
            ViewData["AcceptedFeedbackRequests"] = _context.FeedbackRequests.Where(feedback => feedback.ResolutionId == id && feedback.Resolved == true).ToArray();
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

                // Get Current User
                var loggedInUserIdentity = (ClaimsIdentity)User.Identity;
                var loggedInUserIdentityId = loggedInUserIdentity.FindFirst(ClaimTypes.NameIdentifier);
                Console.Write("\nCurrentId: " + loggedInUserIdentityId.Value + "\n");

                // Add Resolution
                resolution.CreationDate = DateTime.Now;
                resolution.Status = "Draft";
                resolution.OwnerUserID = loggedInUserIdentityId.Value;
                _context.Add(resolution);
                await _context.SaveChangesAsync();

                // Add Feedback Requests
                IdentityUser[] boardMembers = _userManager.GetUsersInRoleAsync("Member").Result.ToArray();
                IdentityUser[] filteredBoardMembers = boardMembers.Where(member => member.Id != loggedInUserIdentityId.Value).ToArray();

                // foreach (var user in filteredBoardMembers)
                // {
                //     Console.Write("filtered: " + user.Id + "\n");
                // }

                foreach (IdentityUser member in filteredBoardMembers)
                {
                    FeedbackRequest feedbackRequest = new FeedbackRequest();
                    feedbackRequest.CreationDate = DateTime.Now;
                    feedbackRequest.Accepted = false;
                    feedbackRequest.Resolved = false;
                    feedbackRequest.Description = "";
                    feedbackRequest.ESignature = "";
                    feedbackRequest.OwnerUserID = member.Id;
                    feedbackRequest.ResolutionId = resolution.ResolutionId;

                    _context.Add(feedbackRequest);
                    await _context.SaveChangesAsync();
                }

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
            var loggedInUserIdentity = (ClaimsIdentity)User.Identity;
            var loggedInUserIdentityId = loggedInUserIdentity.FindFirst(ClaimTypes.NameIdentifier);
            FeedbackRequest[] feedbackRequests = (from FeedbackRequest in _context.FeedbackRequests select FeedbackRequest).ToArray();
            foreach (FeedbackRequest feedbackRequest in feedbackRequests)
            {
                Console.Write(feedbackRequest.ResolutionId + "\n");
                if (feedbackRequest.ResolutionId == id && feedbackRequest.OwnerUserID == loggedInUserIdentityId.Value)
                {
                    Console.Write("Id found: " + feedbackRequest.ResolutionId + "\n\n");
                    return RedirectToAction("Edit", "FeedbackRequests", new { id = feedbackRequest.FeedbackRequestId });
                }
            }
            return NotFound();
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
            FeedbackRequest[] feedbackRequests = (from FeedbackRequest in _context.FeedbackRequests select FeedbackRequest).ToArray();
            if (resolution != null)
            {
                if (resolution.Status == "Draft")
                {
                    for (int i = 0; i < feedbackRequests.Count(); i++)
                    {
                        if (feedbackRequests[i].ResolutionId == id)
                        {
                            _context.FeedbackRequests.Remove(feedbackRequests[i]);
                        }
                    }
                    await _context.SaveChangesAsync();
                    _context.Resolutions.Remove(resolution);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ResolutionExists(int? id)
        {
            return (_context.Resolutions?.Any(e => e.ResolutionId == id)).GetValueOrDefault();
        }
    }
}
