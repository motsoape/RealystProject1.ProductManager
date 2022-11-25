using Microsoft.AspNetCore.Mvc;
using ProductManager.Repositories.Entities;
using ProductManager.Services;
using ProductManager.Services.Interfaces;
using ProductManager.Services.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.NetworkInformation;

namespace ProductManager.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IStats _stats;

        public StatsController(IStats stats)
        {
            _stats = stats;
        }

        // GET: api/Stats
        [HttpGet]
        [SwaggerOperation("GetStats")]
        public async Task<StatsModel> Get()
        {
            var results = await _stats.GetStats();
            return results;
        }
    }
}
