using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResolutionManagement.Controllers;
using ResolutionManagement.Data;

namespace ResolutionManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class UserController : Controller
    {
        private static IdentityUser currentUser = new IdentityUser();
        private static IdentityRole currentRole = new IdentityRole();
        private ApplicationDbContext _context;
        private readonly ILogger<ResolutionController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ILogger<ResolutionController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            // List<IdentityRole> roles = _userManager.GetRolesAsync();
            List<IdentityRole> roles = _roleManager.Roles.ToList();
            List<IdentityUser> users = _userManager.Users.ToList();
            // ViewData["users"] = users;
            ViewData["Users"] = users;
            return View(users);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            currentUser = await _userManager.FindByIdAsync(id);
            Console.WriteLine("User: " + currentUser.UserName);
            ViewData["User"] = currentUser;
            var userRoles = await _context.UserRoles.ToArrayAsync();
            foreach (var userRole in userRoles)
            {
                if (userRole.UserId == user.Id)
                {
                    var role = await _context.Roles.FindAsync(userRole.RoleId);
                    ViewData["Role"] = role.Name;
                    currentRole = role;
                    return View(role);
                }
            }
            currentRole = new IdentityRole();
            ViewData["Role"] = "n/a";
            if (user == null) return NotFound();
            return View(new IdentityRole());
        }

        // POST: Resolution/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,NormalizedName,ConcurrentStamp")] IdentityRole role)
        {
            // Console.WriteLine(currentUser.UserName);
            Console.WriteLine("role: " + role.Name);
            role.NormalizedName = role.Name.ToUpper();
            Console.WriteLine("role: " + role.NormalizedName);
            if (role.Name == "n/a")
            {
                return RedirectToAction(nameof(Index));
            }
            IdentityRole roleObj = await _roleManager.FindByNameAsync(role.Name);
            if (roleObj == null)
            {
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("Current user: " + currentUser.UserName);
            IdentityUser user = await _userManager.FindByIdAsync(currentUser.Id);
            Console.WriteLine("UserName: " + user.UserName);
            if (currentRole == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (currentRole.Name == "Admin")
            {
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("Current role: " + currentRole.Name);
            Console.WriteLine("Current role normalized: " + currentRole.NormalizedName);
            if (currentRole.Name != null && currentRole.Name == "")
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole.Name);
            }
            await _userManager.AddToRoleAsync(user, roleObj.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}