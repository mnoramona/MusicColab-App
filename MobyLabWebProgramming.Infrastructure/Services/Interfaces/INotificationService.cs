using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface INotificationService
{
    Task<ServiceResponse<NotificationDTO>> GetNotification(Guid id, CancellationToken cancellationToken = default);
    Task<ServiceResponse<PagedResponse<NotificationDTO>>> GetNotifications(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    Task<ServiceResponse<int>> GetNotificationCount(CancellationToken cancellationToken = default);
    Task<ServiceResponse> AddNotification(NotificationDTO notification, UserDTO requestingUser, CancellationToken cancellationToken = default);

    Task<ServiceResponse> UpdateNotification(NotificationDTO notification, CancellationToken cancellationToken = default);
    Task<ServiceResponse> DeleteNotification(Guid id, CancellationToken cancellationToken = default);
}