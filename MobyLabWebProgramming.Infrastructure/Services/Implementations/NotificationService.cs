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

public class NotificationService(IRepository<WebAppDatabaseContext> repository) : INotificationService
{
    public async Task<ServiceResponse<NotificationDTO>> GetNotification(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new NotificationProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse.ForSuccess(result) :
            ServiceResponse.FromError<NotificationDTO>(CommonErrors.UserNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<NotificationDTO>>> GetNotifications(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await repository.PageAsync(pagination, new NotificationProjectionSpec(pagination.Search), cancellationToken);
        return ServiceResponse.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetNotificationCount(CancellationToken cancellationToken = default) =>
        ServiceResponse.ForSuccess(await repository.GetCountAsync<Notification>(cancellationToken));

    public async Task<ServiceResponse> AddNotification(NotificationDTO notification, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        await repository.AddAsync(new Notification
        {
            Title = notification.Title,
            Message = notification.Message,
            IsRead = notification.IsRead,
            CreatedAt = DateTime.UtcNow,
            UserId = requestingUser.Id
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }



    public async Task<ServiceResponse> UpdateNotification(NotificationDTO notification, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetAsync(new NotificationSpec(notification.Id), cancellationToken);

        if (entity != null)
        {
            entity.Title = notification.Title;
            entity.Message = notification.Message;
            entity.IsRead = notification.IsRead;
            entity.CreatedAt = notification.CreatedAt;

            await repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteNotification(Guid id, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync<Notification>(id, cancellationToken);
        return ServiceResponse.ForSuccess();
    }
}
