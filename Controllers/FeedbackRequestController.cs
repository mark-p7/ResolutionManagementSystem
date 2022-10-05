using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResolutionManagement.Data;
using ResolutionManagement.Models;

namespace ResolutionManagement.Controllers
{
    [Route("[controller]")]
    public class FeedbackRequestController : Controller
    {
        private ApplicationDbContext _context;
        private readonly ILogger<FeedbackRequestController> _logger;

        public FeedbackRequestController(ILogger<FeedbackRequestController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return _context.Resolutions != null ? 
                        View(await _context.FeedbackRequests.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Resolutions'  is null.");
        }
        
        [Route("Redirect/{ResolutionId}")]
        public IActionResult FeedbackRedirection(int ResolutionId)
        {
            Console.Write("Hit the Feedback Redirection Route");
            FeedbackRequest[] feedbackRequests = (from FeedbackRequest in _context.FeedbackRequests select FeedbackRequest).ToArray();
            foreach (FeedbackRequest feedbackRequest in feedbackRequests)
            {
                if (feedbackRequest.ResolutionId == ResolutionId) {
                    return RedirectToAction("FeedbackRequest", new { feedbackRequest.FeedbackRequestId });
                }
            }
            return RedirectToAction("Error", "FeedbackRequestController");
        }

        [Route("FeedbackRequest/{FeedbackRequestId}")]
        public IActionResult FeedbackRequest(int FeedbackRequestId)
        {   
            Console.Write("Hit the Feedback Request Route");
            FeedbackRequest feedback = _context.FeedbackRequests.Find(FeedbackRequestId);
            ViewData["Feedback"] = feedback;
            return View();
        }

        //         // GET: Resolution/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null || _context.Resolutions == null)
        //     {
        //         return NotFound();
        //     }

        //     var resolution = await _context.Resolutions.FindAsync(id);
        //     if (resolution == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["ResolutionId"] = new SelectList(_context.Resolutions, "ResolutionId", "Abstract", resolution.ResolutionId);
        //     return View(resolution);
        // }

        // // POST: Feedback/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int? id, [Bind("ResolutionId,CreationDate,Abstract,Status,OwnerUserID")] Resolution resolution)
        // {
        //     if (id != resolution.ResolutionId)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(resolution);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!ResolutionExists(resolution.ResolutionId))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(resolution);
        // }

        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}