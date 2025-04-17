using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProjectService
{
    public Task<ServiceResponse<ProjectDTO>> GetProject(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<ProjectDTO>>> GetProjects(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetProjectCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddProject(ProjectAddDTO project, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateProject(UpdateProjectDTO project, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteProject(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default);
}