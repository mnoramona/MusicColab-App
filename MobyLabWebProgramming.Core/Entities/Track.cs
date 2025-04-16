using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Track : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string FilePath { get; set; } = default!;
        public TimeSpan Duration { get; set; }
        public string Status { get; set; } = default!;

        // Foreign keys
        public Guid ProjectId { get; set; }
        public Guid CreatorId { get; set; }

        // Navigation properties
        public Project Project { get; set; } = default!;
        public User Creator { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}