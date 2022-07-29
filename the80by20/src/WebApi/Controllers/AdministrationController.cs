using Common;
using Core.App.Administration.MasterData;
using Core.Infrastructure.DAL.Administration;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
// info CancellationToken can be passed in controller action method, passed down to async/await ef methods
[ApiController]
[Route("api/[controller]")]
public class AdministrationController : ControllerBase
{
    private readonly ILogger<AdministrationController> _logger;
    private readonly IGenericRepository<Category> _categoryCrud;

    public AdministrationController(ILogger<AdministrationController> logger,
        IGenericRepository<Category> categoryCrud)
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