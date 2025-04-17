using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// Specification pentru extragerea track-urilor raw din baza de date.
/// </summary>
public sealed class TrackSpec : Specification<Track>
{
    public TrackSpec(Guid id) => Query.Where(e => e.Id == id);

    public TrackSpec(string title) => Query.Where(e => e.Title == title);
}