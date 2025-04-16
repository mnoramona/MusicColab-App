namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class UploadTrackDTO
{
    public string Title { get; set; } = string.Empty;
    public int DurationInSeconds { get; set; }
    public Guid ProjectId { get; set; }
}