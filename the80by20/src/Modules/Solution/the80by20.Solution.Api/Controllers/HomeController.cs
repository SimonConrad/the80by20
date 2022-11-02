using Microsoft.AspNetCore.Mvc;

namespace the80by20.Masterdata.Api.Controllers
{
    [Route("solution-to-problem")]
    internal class HomeController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Solution To Problem API";
    }
}
