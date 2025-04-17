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
public class ProjectController : AuthorizedController
{
    private readonly IProjectService _projectService;

    public ProjectController(IUserService userService, IProjectService projectService) : base(userService)
    {
        _projectService = projectService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Create([FromBody] ProjectAddDTO dto)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Error != null)
        {
            return ErrorMessageResult(currentUser.Error);
        }

        var result = await _projectService.AddProject(dto, currentUser.Result!, CancellationToken.None);
        return result.Error != null ? ErrorMessageResult(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ProjectDTO>>> GetById([FromRoute] Guid id)
    {
        var result = await _projectService.GetProject(id);
        return result.Error != null ? ErrorMessageResult<ProjectDTO>(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ProjectDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var result = await _projectService.GetProjects(pagination);
        return result.Error != null ? ErrorMessageResult<PagedResponse<ProjectDTO>>(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Update([FromRoute] Guid id, [FromBody] UpdateProjectDTO dto)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Error != null)
        {
            return ErrorMessageResult(currentUser.Error);
        }

        dto.Id = id;
        var result = await _projectService.UpdateProject(dto, currentUser.Result!, CancellationToken.None);
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

        var result = await _projectService.DeleteProject(id, currentUser.Result!, CancellationToken.None);
        return result.Error != null ? ErrorMessageResult(result.Error) : FromServiceResponse(result);
    }
}