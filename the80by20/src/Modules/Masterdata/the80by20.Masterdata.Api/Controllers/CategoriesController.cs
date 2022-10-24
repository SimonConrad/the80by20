﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using the80by20.Masterdata.App.DTO;
using the80by20.Masterdata.App.Services;

// todo podzaił kontrolerów jak modułów

namespace the80by20.Masterdata.Api.Controllers;
// info CancellationToken can be passed in controller action method, passed down to async/await ef methods
[ApiController]
[Authorize(Policy = "is-admin")]
internal class CategoriesController : BaseController
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly ICategoryService categoryService;

    public CategoriesController(ILogger<CategoriesController> logger,
        ICategoryService categoryService)
    {
        _logger = logger;
        this.categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> Get() 
        => Ok(await categoryService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryDetailsDto>> Get(Guid id) 
        => OkOrNotFound(await categoryService.GetAsync(id));


    [HttpPost]
    public async Task<ActionResult> Add(CategoryDto dto)
    {
        await categoryService.AddAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, value: null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, CategoryDetailsDto dto)
    {
        dto.Id = id;
        await categoryService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await categoryService.DeleteAsync(id);
        return NoContent();
    }
}
