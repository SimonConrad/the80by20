using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using the80by20.App.MasterData;
using the80by20.App.MasterData.CategoryCrud;

// todo podzaił kontrolerów jak modułów

namespace the80by20.WebApi.MasterData;
// info CancellationToken can be passed in controller action method, passed down to async/await ef methods
[ApiController]
[Authorize(Policy = "is-admin")]
[Route("master-data/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ILogger<CategoriesController> _logger;
    private readonly IGenericRepository<Category> _categoryCrud;

    public CategoriesController(ILogger<CategoriesController> logger,
        IGenericRepository<Category> categoryCrud)
    {
        _logger = logger;
        _categoryCrud = categoryCrud;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        await _categoryCrud.Add(category);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await _categoryCrud.GetAll();
        return Ok(res);
    }
}