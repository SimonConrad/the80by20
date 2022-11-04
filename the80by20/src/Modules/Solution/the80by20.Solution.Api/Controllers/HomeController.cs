using Microsoft.AspNetCore.Mvc;

namespace the80by20.Modules.Solution.Api.Controllers
{
    [Route(SolutionModule.BasePath)]
    internal class HomeController : BaseController
    {
        [HttpGet]
        public ActionResult<string> Get() => "Solution To Problem API";
    }
}
