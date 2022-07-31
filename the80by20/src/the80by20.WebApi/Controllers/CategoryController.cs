using Microsoft.AspNetCore.Mvc;
using the80by20.App;
using the80by20.App.MasterData;
using the80by20.App.MasterData.CategoryCrud;

// todo podzaił kontrolerów jak modułów

namespace the80by20.WebApi.Controllers;
// info CancellationToken can be passed in controller action method, passed down to async/await ef methods
[ApiController]
[Route("master-data")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IGenericRepository<Category> _categoryCrud;

    public CategoryController(ILogger<CategoryController> logger,
        IGenericRepository<Category> categoryCrud)
    {
        _logger = logger;
        _categoryCrud = categoryCrud;
    }

    [HttpPost("category")]
    public async Task<IActionResult> Create(Category category)
    {
        await _categoryCrud.Add(category);
        return Ok();
    }
    
    [HttpGet("categories")]
    public async Task<IActionResult> Get()
    {
        var res = await _categoryCrud.GetAll();
        return Ok(res);
    }
}