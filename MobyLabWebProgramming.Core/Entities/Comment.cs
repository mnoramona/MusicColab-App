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

        // Foreign keys
        public Guid AuthorId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TrackId { get; set; }

        // Navigation properties
        public User Author { get; set; } = default!;
        public Project? Project { get; set; }
        public Track? Track { get; set; }
    }
}