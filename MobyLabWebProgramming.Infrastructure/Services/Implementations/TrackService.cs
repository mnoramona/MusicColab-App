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

public class TrackService(IRepository<WebAppDatabaseContext> repository) : ITrackService
{
    public async Task<ServiceResponse<TrackDTO>> GetTrack(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new TrackProjectionSpec(id), cancellationToken);
        return result != null ? ServiceResponse.ForSuccess(result) : ServiceResponse.FromError<TrackDTO>(new(HttpStatusCode.NotFound, "Track not found!"));
    }

    public async Task<ServiceResponse<PagedResponse<TrackDTO>>> GetTracks(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await repository.PageAsync(pagination, new TrackProjectionSpec(pagination.Search), cancellationToken);
        return ServiceResponse.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetTrackCount(CancellationToken cancellationToken = default) =>
        ServiceResponse.ForSuccess(await repository.GetCountAsync<Track>(cancellationToken));

    public async Task<ServiceResponse> UploadTrack(AddTrackDTO track, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        await repository.AddAsync(new Track
        {
            Title = track.Title,
            Description = track.Description,
            FilePath = track.FilePath,
            Duration = track.Duration,
            Status = "Pending",
            ProjectId = track.ProjectId,
            CreatorId = requestingUser.Id
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateTrack(UpdateTrackDTO track, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetAsync(new TrackSpec(track.Id), cancellationToken);

        if (entity != null)
        {
            entity.Title = track.Title ?? entity.Title;
            entity.Description = track.Description ?? entity.Description;
            entity.Status = track.Status ?? entity.Status;
            entity.Duration = track.Duration ?? entity.Duration;

            await repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteTrack(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync<Track>(id, cancellationToken);
        return ServiceResponse.ForSuccess();
    }
}
