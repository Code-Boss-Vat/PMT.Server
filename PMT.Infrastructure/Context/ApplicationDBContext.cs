using Microsoft.EntityFrameworkCore;
using PMT.Core.Entities;

namespace PMT.Infrastructure.Context;

public class ApplicationDBContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrgAdminMap>()
            .HasKey(x => new { x.OrganizationId, x.AdminId });
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrgAdminMap> OrgAdminMaps { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }
}
