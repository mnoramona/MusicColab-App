using System;

namespace MobyLabWebProgramming.Core.Entities;

public class UserProject : BaseEntity
{
    // Foreign keys
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }

    // Navigation properties
    public User User { get; set; } = default!;
    public Project Project { get; set; } = default!;
}
// public string PermissionLevel { get; set; } = "Collaborator"; // e.g., "Viewer", "Editor", etc.