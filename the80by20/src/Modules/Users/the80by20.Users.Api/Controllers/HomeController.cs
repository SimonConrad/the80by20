using Microsoft.AspNetCore.Mvc;

namespace the80by20.Masterdata.Api.Controllers
{
    [Route("users")]
    internal class HomeController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Users API";
    }
}
