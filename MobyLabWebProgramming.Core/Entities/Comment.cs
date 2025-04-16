using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

        // Foreign keys
        public Guid AuthorId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TrackId { get; set; }

        // Navigation properties
        // Many-to-one: Comment -> User (author)
        public User Author { get; set; } = default!;

        // Many-to-one: Comment -> Project (optional)
        public Project? Project { get; set; }

        // Many-to-one: Comment -> Track (optional)
        public Track? Track { get; set; }
    }
}