﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : AuthorizedController
{
    private readonly ICommentService _commentService;

    public CommentController(IUserService userService, ICommentService commentService) : base(userService)
    {
        _commentService = commentService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Create([FromBody] UpdateCommentDTO dto)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Error != null)
        {
            return ErrorMessageResult(currentUser.Error);
        }

        var result = await _commentService.AddComment(dto, CancellationToken.None);
        return result.Error != null ? ErrorMessageResult(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<CommentDTO>>> GetById([FromRoute] Guid id)
    {
        var result = await _commentService.GetComment(id);
        return result.Error != null ? ErrorMessageResult<CommentDTO>(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<CommentDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var result = await _commentService.GetComments(pagination);
        return result.Error != null ? ErrorMessageResult<PagedResponse<CommentDTO>>(result.Error) : FromServiceResponse(result);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Update([FromRoute] Guid id, [FromBody] UpdateCommentDTO dto)
    {
        var currentUser = await GetCurrentUser();
        if (currentUser.Error != null)
        {
            return ErrorMessageResult(currentUser.Error);
        }

        dto.Id = id;
        var result = await _commentService.UpdateComment(dto, CancellationToken.None);
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

        var result = await _commentService.DeleteComment(id, CancellationToken.None);
        return result.Error != null ? ErrorMessageResult(result.Error) : FromServiceResponse(result);
    }
}