using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICommentService
{
    Task<ServiceResponse<CommentDTO>> GetComment(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceResponse<PagedResponse<CommentDTO>>> GetComments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    Task<ServiceResponse<int>> GetCommentCount(CancellationToken cancellationToken = default);
    Task<ServiceResponse> AddComment(UpdateCommentDTO comment, CancellationToken cancellationToken = default);
    Task<ServiceResponse> UpdateComment(UpdateCommentDTO comment, CancellationToken cancellationToken = default);
    Task<ServiceResponse> DeleteComment(Guid id, CancellationToken cancellationToken = default);
}