using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResolutionManagement.Models;
using ResolutionManagementSystem.Data;

namespace ResolutionManagement.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Resolution> Resolutions { get; set; }
    public DbSet<FeedbackRequest> FeedbackRequests { get; set; }
    public object Resolution { get; internal set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Resolution>().ToTable("Resolution");
        builder.Entity<FeedbackRequest>().ToTable("FeedbackRequest"); 
        builder.Seed();
    }
}
