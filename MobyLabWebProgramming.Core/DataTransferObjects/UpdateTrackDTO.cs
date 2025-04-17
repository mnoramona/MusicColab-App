namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class UpdateTrackDTO
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public TimeSpan? Duration { get; set; }
    public string? Status { get; set; }
    public Guid ProjectId { get; set; }
}