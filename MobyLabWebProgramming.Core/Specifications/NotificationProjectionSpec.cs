using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class NotificationProjectionSpec : Specification<Notification, NotificationDTO>
{
    public NotificationProjectionSpec(bool orderByCreatedAt = false)
    {
        if (orderByCreatedAt)
        {
            Query.OrderByDescending(e => e.CreatedAt);
        }

        Query.Select(e => new NotificationDTO
        {
            Id = e.Id,
            Title = e.Title,
            Message = e.Message,
            IsRead = e.IsRead,
            CreatedAt = e.CreatedAt
        });
    }

    public NotificationProjectionSpec(Guid id) : this() =>
        Query.Where(e => e.Id == id);

    public NotificationProjectionSpec(string? search) : this(true)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
            return;

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Title, searchExpr));
    }
}