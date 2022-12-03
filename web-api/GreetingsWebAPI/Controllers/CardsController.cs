using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace GreetingsWebAPI.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> ProcessCard([FromBody] string card)
        {
            int random_value = RandomNumberGenerator.GetInt32(100);
            var approved = random_value > 10;
            await Task.Delay(TimeSpan.FromSeconds(1));
            System.Diagnostics.Debug.WriteLine($"Card {card} is processed and it is {approved}");
            return Ok(new { Card = card, Approved = approved });
        }
    }
}
