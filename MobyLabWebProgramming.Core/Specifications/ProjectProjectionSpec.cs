using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// Specification pentru proiecția proiectelor direct pe ProjectDTO.
/// </summary>
public sealed class ProjectProjectionSpec : Specification<Project, ProjectDTO>
{
    public ProjectProjectionSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new ProjectDTO
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                CreatedAt = e.CreatedAt
            })
            .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);

    public ProjectProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id);

    public ProjectProjectionSpec(string? search) : this(true)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Title, searchExpr) ||
                         (e.Description != null && EF.Functions.ILike(e.Description, searchExpr)));
    }
}