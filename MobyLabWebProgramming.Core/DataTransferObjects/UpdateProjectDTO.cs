﻿namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class UpdateProjectDTO
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Status { get; set; }
}