using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITrackService
{
    public Task<ServiceResponse<TrackDTO>> GetTrack(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<TrackDTO>>> GetTracks(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetTrackCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UploadTrack(AddTrackDTO track, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateTrack(UpdateTrackDTO track, UserDTO requestingUser, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteTrack(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default);
}