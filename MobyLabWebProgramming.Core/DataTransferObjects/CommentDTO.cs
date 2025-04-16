using Microsoft.AspNetCore.Mvc;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CommentDTO
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime PostedAt { get; set; }
    public Guid TrackId { get; set; }
}