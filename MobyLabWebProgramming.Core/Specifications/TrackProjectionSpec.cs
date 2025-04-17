using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TrackProjectionSpec : Specification<Track, TrackDTO>
{
    public TrackProjectionSpec(bool orderByCreatedAt = false)
    {
        if (orderByCreatedAt)
        {
            Query.OrderByDescending(e => e.CreatedAt);
        }

        Query.Select(e => new TrackDTO
        {
            Id = e.Id,
            Title = e.Title,
            DurationInSeconds = (int)e.Duration.TotalSeconds,
            UploadedAt = e.CreatedAt,
            ProjectId = e.ProjectId
        });
    }

    public TrackProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);

    public TrackProjectionSpec(string? search) : this(true)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Title, searchExpr));
    }
}