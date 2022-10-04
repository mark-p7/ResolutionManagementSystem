using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResolutionManagement.Models;
using ResolutionManagement.Data;

namespace ResolutionManagement.Controllers
{
    [Route("[controller]")]
    public class ResolutionController : Controller
    {
        private ApplicationDbContext _db;
        private readonly ILogger<ResolutionController> _logger;

        public ResolutionController(ILogger<ResolutionController> logger, ApplicationDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["List"] = (from Resolution in _db.Resolutions select Resolution).ToArray();
            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}