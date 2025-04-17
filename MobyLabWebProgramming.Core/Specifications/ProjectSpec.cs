using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// Specification pentru extragerea proiectelor fără proiecție, doar entități raw.
/// </summary>
public sealed class ProjectSpec : Specification<Project>
{
    public ProjectSpec(Guid id) => Query.Where(e => e.Id == id);

    public ProjectSpec(string title) => Query.Where(e => e.Title == title);
}