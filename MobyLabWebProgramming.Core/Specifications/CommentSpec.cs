using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CommentSpec : Specification<Comment>
{
    public CommentSpec(Guid id) => Query.Where(e => e.Id == id);
}