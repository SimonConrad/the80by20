using Microsoft.AspNetCore.Mvc;

namespace the80by20.Masterdata.Api.Controllers
{
    [Route("master-data")]
    internal class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Master Data API";
    }
}
