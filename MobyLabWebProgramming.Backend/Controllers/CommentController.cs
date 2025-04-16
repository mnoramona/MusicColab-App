using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    [HttpPost]
    public IActionResult Add([FromBody] AddCommentDTO dto)
    {
        var comment = new CommentDTO
        {
            Id = Guid.NewGuid(),
            Text = dto.Text,
            AuthorName = dto.AuthorName,
            PostedAt = DateTime.UtcNow,
            TrackId = dto.TrackId
        };

        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var comment = new CommentDTO
        {
            Id = id,
            Text = "Comentariu demo",
            AuthorName = "User demo",
            PostedAt = DateTime.UtcNow,
            TrackId = Guid.NewGuid()
        };

        return Ok(comment);
    }
}