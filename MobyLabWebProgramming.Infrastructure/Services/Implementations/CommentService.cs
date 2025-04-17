using System.Net;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class CommentService(IRepository<WebAppDatabaseContext> repository, IUserService userService) : ICommentService
{
    public async Task<ServiceResponse<CommentDTO>> GetComment(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new CommentProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse.ForSuccess(result) :
            ServiceResponse.FromError<CommentDTO>(CommonErrors.UserNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<CommentDTO>>> GetComments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await repository.PageAsync(pagination, new CommentProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetCommentCount(CancellationToken cancellationToken = default) =>
        ServiceResponse.ForSuccess(await repository.GetCountAsync<Comment>(cancellationToken));

    public async Task<ServiceResponse> AddComment(UpdateCommentDTO comment, CancellationToken cancellationToken = default)
    {
        var author = await userService.GetUser(comment.AuthorId, cancellationToken);

        if (author.Result == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "Author not found!", ErrorCodes.EntityNotFound));
        }

        var entity = new Comment
        {
            Content = comment.Text,
            AuthorId = comment.AuthorId,
            TrackId = comment.TrackId,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateComment(UpdateCommentDTO comment, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetAsync(new CommentSpec(comment.Id), cancellationToken);

        if (entity != null)
        {
            entity.Content = comment.Text;

            await repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteComment(Guid id, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync<Comment>(id, cancellationToken);
        return ServiceResponse.ForSuccess();
    }
}
