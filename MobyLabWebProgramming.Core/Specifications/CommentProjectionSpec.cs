using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CommentProjectionSpec : Specification<Comment, CommentDTO>
{
    public CommentProjectionSpec(bool orderByCreatedAt = false)
    {
        Query.Include(e => e.Author);

        if (orderByCreatedAt)
        {
            Query.OrderByDescending(e => e.CreatedAt);
        }

        Query.Select(e => new CommentDTO
        {
            Id = e.Id,
            Text = e.Content,
            AuthorName = e.Author.Name,
            PostedAt = e.CreatedAt,
            TrackId = e.TrackId ?? Guid.Empty
        });
    }


    public CommentProjectionSpec(Guid id) : this()
    {
        Query.Where(e => e.Id == id);
    }

    public CommentProjectionSpec(string? search) : this(true)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Content, searchExpr));
    }
}