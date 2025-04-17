using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IUserService _userService;

    public CommentController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromBody] UpdateCommentDTO dto)
    {
        var authorResponse = await _userService.GetUser(dto.AuthorId);
        var authorName = authorResponse?.Result?.Name ?? "Unknown Author";

        var comment = new CommentDTO
        {
            Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
            Text = dto.Text,
            AuthorName = authorName,
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
            Text = "Demo comment text",
            AuthorName = "Demo Author",
            PostedAt = DateTime.UtcNow,
            TrackId = Guid.NewGuid()
        };

        return Ok(comment);
    }
}