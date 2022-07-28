using Common;
using Core.App.Administration;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdministrationController : ControllerBase
{
    private readonly ILogger<AdministrationController> _logger;
    private readonly ICategoryRepository _categoryCrud;

    public AdministrationController(ILogger<AdministrationController> logger,
        ICategoryRepository categoryCrud)
    {
        _logger = logger;
        _categoryCrud = categoryCrud;
    }

    [HttpPost("/category")]
    public async Task<IActionResult> Create(Category category)
    {
        await _categoryCrud.Add(category);
        return Ok();
    }
    
    [HttpGet("/categories")]
    public async Task<IActionResult> Get()
    {
        var res = await _categoryCrud.GetAll();
        return Ok(res);
    }
}