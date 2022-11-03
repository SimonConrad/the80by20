using Microsoft.AspNetCore.Mvc;

namespace the80by20.Modules.Users.Api.Controllers
{
    [Route(UsersModule.BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Users API";
    }
}
