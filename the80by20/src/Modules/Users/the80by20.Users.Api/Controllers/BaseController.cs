using Microsoft.AspNetCore.Mvc;
using the80by20.Shared.Infrastucture.Api;

namespace the80by20.Modules.Users.Api.Controllers
{

    [ApiController] // info  bacouse of inheriting from ControllerBase and marking controllre as [ApiController] attributes: FromRoute, FromQuery, FromBody can be removed
    [ProducesDefaultContentType]
    [Route(UsersModule.BasePath + "/[controller]")]
    internal abstract class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is not null)
            {
                return Ok(model);
            }

            return NotFound();
        }
    }
}
