using Ardalis.EFCore.Extensions; // Asigură-te că ai instalat Ardalis.Specification.EntityFrameworkCore
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Infrastructure.Database.Configurations;
using MobyLabWebProgramming.Infrastructure.EntityConfigurations;

namespace MobyLabWebProgramming.Infrastructure.Database;

/// <summary>
/// This is the database context used to connect with the database and links the ORM, Entity Framework, with it.
/// </summary>
public sealed class WebAppDatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Project> Projects { get; set; } = default!;
    public DbSet<Track> Tracks { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;
    public DbSet<Notification> Notifications { get; set; } = default!;
    public DbSet<UserProject> UserProjects { get; set; } = default!;
    public DbSet<UserFile> UserFiles { get; set; } = default!;

    public WebAppDatabaseContext(DbContextOptions<WebAppDatabaseContext> options, bool migrate = true) : base(options)
    {
        if (migrate)
        {
            Database.Migrate();
        }
    }

    /// <summary>
    /// Here additional configuration for the ORM is performed.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Applies PostgreSQL specific extensions if using Postgres
        modelBuilder.HasPostgresExtension("unaccent");

        // Apply configurations explicitly
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserFileConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new TrackConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new UserProjectConfiguration());

        // Or alternatively you could scan the whole assembly (you already had this):
        modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
    }
}
