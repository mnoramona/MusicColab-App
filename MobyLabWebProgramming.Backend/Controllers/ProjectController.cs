using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] CreateProjectDTO dto)
    {
        var project = new ProjectDTO
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(new ProjectDTO
        {
            Id = id,
            Title = "Mock Project",
            Description = "Descriere demo",
            CreatedAt = DateTime.UtcNow
        });
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] UpdateProjectDTO dto)
    {
        // Simulează actualizarea proiectului cu ID-ul dat (mock)
        var updatedProject = new ProjectDTO
        {
            Id = id,
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow // păstrăm o dată nouă ca exemplu
        };

        // Răspuns cu 200 OK și proiectul actualizat (doar ca demo)
        return Ok(updatedProject);
    }


}