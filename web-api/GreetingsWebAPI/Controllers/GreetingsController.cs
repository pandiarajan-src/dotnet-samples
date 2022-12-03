using Microsoft.AspNetCore.Mvc;

namespace GreetingsWebAPI.Controllers
{
    [Route("api/greetings")]
    [ApiController]
    public class GreetingsController : ControllerBase
    {
        [HttpGet("{name}")]
        public ActionResult<string> GetGreeting(string name)
        {
            return $"Hello {name}";
        }
    }
}
