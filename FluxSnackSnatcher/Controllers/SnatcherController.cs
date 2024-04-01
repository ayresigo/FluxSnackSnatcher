﻿using FluxSnackSnatcher.Facades;
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
        public async Task<ActionResult<string>> SnatchSnack(string url, string? snacks)
        {
            var response = await _snatcherFacade.SnatchSnack(snacks, url);

            return Ok(response);
        }

        [HttpGet("snacks")]
        public async Task<ActionResult<IList<CookieData>>> GetSnacks()
        {
            var snacks = await _snatcherFacade.GetSnacks();

            return Ok(snacks);
        }
    }
}