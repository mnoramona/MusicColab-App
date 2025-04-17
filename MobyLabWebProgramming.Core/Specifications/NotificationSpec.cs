using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class NotificationSpec : Specification<Notification>
{
    public NotificationSpec(Guid id) => Query.Where(e => e.Id == id);

    public NotificationSpec(string title) => Query.Where(e => e.Title == title);
}