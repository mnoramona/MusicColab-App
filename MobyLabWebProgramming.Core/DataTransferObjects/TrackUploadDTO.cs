namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TrackUploadDTO
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string FilePath { get; set; } = default!;
    public TimeSpan Duration { get; set; }
    public Guid ProjectId { get; set; }
}