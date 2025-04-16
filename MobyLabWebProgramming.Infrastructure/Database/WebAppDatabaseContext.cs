using Ardalis.EFCore.Extensions; // Ensure this package is installed and compatible with your project
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;

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

        // Ensure the extension is supported by your database provider
        modelBuilder.HasPostgresExtension("unaccent")
            .ApplyAllConfigurationsFromCurrentAssembly();
    }
}