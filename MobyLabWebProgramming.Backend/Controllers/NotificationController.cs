using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : AuthorizedController
{
    private readonly INotificationService _notificationService;

    public NotificationController(IUserService userService, INotificationService notificationService) : base(userService)
    {
        _notificationService = notificationService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Create([FromBody] NotificationDTO dto)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Error != null)
        {
            return ErrorMessageResult(currentUser.Error);
        }

        var result = await _notificationService.AddNotification(dto, currentUser.Result!, CancellationToken.None);
        return result.Error != null ? ErrorMessageResult(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<NotificationDTO>>> GetById([FromRoute] Guid id)
    {
        var result = await _notificationService.GetNotification(id);
        return result.Error != null ? ErrorMessageResult<NotificationDTO>(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<NotificationDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var result = await _notificationService.GetNotifications(pagination);
        return result.Error != null ? ErrorMessageResult<PagedResponse<NotificationDTO>>(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Update([FromRoute] Guid id, [FromBody] NotificationDTO dto)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Error != null)
        {
            return ErrorMessageResult(currentUser.Error);
        }

        dto.Id = id;
        var result = await _notificationService.UpdateNotification(dto, CancellationToken.None);
        return result.Error != null ? ErrorMessageResult(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Error != null)
        {
            return ErrorMessageResult(currentUser.Error);
        }

        var result = await _notificationService.DeleteNotification(id, CancellationToken.None);
        return result.Error != null ? ErrorMessageResult(result.Error) : FromServiceResponse(result);
    }
} 