using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrackController : ControllerBase
{
    [HttpPost]
    public IActionResult Upload([FromBody] UpdateTrackDTO dto)
    {
        if (dto.Duration == null)
        {
            return BadRequest("Duration is required.");
        }

        var track = new TrackDTO
        {
            Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
            Title = dto.Title ?? "Untitled Track",
            DurationInSeconds = (int)dto.Duration.Value.TotalSeconds,
            UploadedAt = DateTime.UtcNow,
            ProjectId = dto.ProjectId
        };

        return CreatedAtAction(nameof(GetById), new { id = track.Id }, track);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var track = new TrackDTO
        {
            Id = id,
            Title = "Demo Track",
            DurationInSeconds = 180,
            UploadedAt = DateTime.UtcNow,
            ProjectId = Guid.NewGuid()
        };

        return Ok(track);
    }
}