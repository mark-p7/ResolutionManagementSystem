using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResolutionManagement.Data;
using ResolutionManagement.Models;

namespace ResolutionManagement.Controllers
{
    [Route("[controller]")]
    public class FeedbackRequestController : Controller
    {
        private ApplicationDbContext _db;
        private readonly ILogger<FeedbackRequestController> _logger;

        public FeedbackRequestController(ILogger<FeedbackRequestController> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("Redirect/{ResolutionId}")]
        public IActionResult FeedbackRedirection(int ResolutionId)
        {
            Console.Write("Hit the Feedback Redirection Route");
            FeedbackRequest[] feedbackRequests = (from FeedbackRequest in _db.FeedbackRequests select FeedbackRequest).ToArray();
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
            FeedbackRequest feedback = _db.FeedbackRequests.Find(FeedbackRequestId);
            ViewData["Feedback"] = feedback;
            return View();
        }

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