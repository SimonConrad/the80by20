using Microsoft.AspNetCore.Mvc;
using the80by20.Shared.Infrastucture.Api;

namespace the80by20.Services.Sale.Api.Controllers;

[ApiController]
[ProducesDefaultContentType]
[Route("[controller]")]
public class BaseController : ControllerBase
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