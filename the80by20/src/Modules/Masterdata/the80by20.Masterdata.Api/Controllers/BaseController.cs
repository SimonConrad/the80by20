using Microsoft.AspNetCore.Mvc;

namespace the80by20.Modules.Masterdata.Api.Controllers;

[ApiController]
[Route(MasterDataModule.BasePath + "/[controller]")] // TODO apply this in all controller
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