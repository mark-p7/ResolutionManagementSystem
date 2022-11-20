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
    public class RoleController : Controller
    {
        private ApplicationDbContext _context;
        private readonly ILogger<ResolutionController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(ILogger<ResolutionController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        // GET: Role

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users;
            foreach (var user in users)
            {
                Console.Write(user.UserName + "\n");
            }
            // List<IdentityRole> roles = _userManager.GetRolesAsync();
            List<IdentityRole> roles = _roleManager.Roles.ToList();
            // ViewData["users"] = users;
            ViewData["Roles"] = roles;
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp")] IdentityRole role)
        {
            IdentityResult roleResult = await _roleManager.CreateAsync(role);
            if (roleResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewData["role"] = new SelectList(_context.Roles, "Id", "Name", role.Id);
            return View(role);
        }

        // POST: Resolution/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] IdentityRole role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }
            if (role.NormalizedName == "ADMIN")
            {
                return View(role);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _roleManager.UpdateAsync(role);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
        // GET: Resolution/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role.Name == "Admin")
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}