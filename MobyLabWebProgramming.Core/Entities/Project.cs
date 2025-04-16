using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Project : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = default!;

        // Foreign keys
        public Guid OwnerId { get; set; }

        // Navigation properties
        // Many-to-one: Project -> User (owner)
        public User Owner { get; set; } = default!;

        // Many-to-many: Project <-> User
        public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();

        // One-to-many: Project -> Tracks
        public ICollection<Track> Tracks { get; set; } = new List<Track>();

        // One-to-many: Project -> Comments
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}