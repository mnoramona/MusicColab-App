﻿using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a specification to filter the user entities and map it to and UserDTO object via the constructors.
/// The specification will project the entity onto a DTO so it isn't tracked by the framework.
/// Note how the constructors call other constructors which can be used to chain them. Also, this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class UserProjectionSpec : Specification<User, UserDTO>
{
    /// <summary>
    /// In this constructor is the projection/mapping expression used to get UserDTO object directly from the database.
    /// </summary>
    public UserProjectionSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new()
        {
            Id = e.Id,
            Email = e.Email,
            Name = e.Name,
            Role = e.Role
        })
        .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);

    public UserProjectionSpec(Guid id) : this() => Query.Where(e => e.Id == id); // This constructor will call the first declared constructor with the default parameter. 

    public UserProjectionSpec(string? search) : this(true) // This constructor will call the first declared constructor with 'true' as the parameter. 
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr)); // This is an example on how database specific expressions can be used via C# expressions.
                                                                                          // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
