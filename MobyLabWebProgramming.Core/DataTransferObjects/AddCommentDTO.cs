namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AddCommentDTO
{
    public string Text { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public Guid TrackId { get; set; }
}