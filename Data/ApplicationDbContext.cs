using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResolutionManagement.Models;

namespace ResolutionManagement.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Resolution> Resolutions { get; set; }

    public DbSet<FeedbackRequest> FeedbackRequests { get; set; }
}
