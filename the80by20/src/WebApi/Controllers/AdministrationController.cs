using Common;
using Core.App.Administration;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdministrationController : ControllerBase
{
    private readonly ILogger<AdministrationController> _logger;
    private readonly IUnitOfWorkOfAdministrationCrud _unitOfWork;

    public AdministrationController(ILogger<AdministrationController> logger,
        IUnitOfWorkOfAdministrationCrud unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("/category")]
    public async Task<IActionResult> Create(Category category)
    {
        await _unitOfWork.CategoryRepository.Add(category);
        await _unitOfWork.Commit();
        return Ok();
    }
    
    [HttpGet("/categories")]
    public async Task<IActionResult> Get()
    {
        var res = await _unitOfWork.CategoryRepository.GetAll();
        return Ok(res);
    }
}