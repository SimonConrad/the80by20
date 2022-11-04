using Microsoft.AspNetCore.Mvc;
using the80by20.Modules.Solution.Api;

namespace the80by20.Modules.Solution.Api.Controllers;

[ApiController]
[Route(SolutionModule.BasePath + "/[controller]")]
internal class BaseController : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is null)
        {
            return NotFound();
        }

        return Ok(model);
    }

    protected void AddResourceIdHeader(Guid id) => Response.Headers.Add("Resource-ID", id.ToString());
}