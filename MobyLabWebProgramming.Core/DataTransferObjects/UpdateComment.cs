namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class UpdateCommentDTO
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public Guid TrackId { get; set; }
}