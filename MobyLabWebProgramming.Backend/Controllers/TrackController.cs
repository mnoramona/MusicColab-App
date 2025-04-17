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
public class TrackController : AuthorizedController
{
    private readonly ITrackService _trackService;

    public TrackController(IUserService userService, ITrackService trackService) : base(userService)
    {
        _trackService = trackService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Upload([FromBody] AddTrackDTO dto)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Error != null)
        {
            return ErrorMessageResult(currentUser.Error);
        }

        var result = await _trackService.UploadTrack(dto, currentUser.Result!, CancellationToken.None);
        return result.Error != null ? ErrorMessageResult(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<TrackDTO>>> GetById([FromRoute] Guid id)
    {
        var result = await _trackService.GetTrack(id);
        return result.Error != null ? ErrorMessageResult<TrackDTO>(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<TrackDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var result = await _trackService.GetTracks(pagination);
        return result.Error != null ? ErrorMessageResult<PagedResponse<TrackDTO>>(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Update([FromRoute] Guid id, [FromBody] UpdateTrackDTO dto)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Error != null)
        {
            return ErrorMessageResult(currentUser.Error);
        }

        dto.Id = id;
        var result = await _trackService.UpdateTrack(dto, currentUser.Result!, CancellationToken.None);
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

        var result = await _trackService.DeleteTrack(id, currentUser.Result!, CancellationToken.None);
        return result.Error != null ? ErrorMessageResult(result.Error) : FromServiceResponse(result);
    }
}