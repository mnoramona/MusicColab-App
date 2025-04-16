namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TrackDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationInSeconds { get; set; }
    public DateTime UploadedAt { get; set; }
    public Guid ProjectId { get; set; }
}