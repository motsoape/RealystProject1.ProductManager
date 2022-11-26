using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly IStatsService _stats;
        private readonly ILogger<StatsController> _logger;

        public StatsController(IStatsService stats, ILogger<StatsController> logger)
        {
            _stats = stats;
            _logger = logger;
        }

        // GET: api/Stats
        [HttpGet]
        [SwaggerOperation("GetStats")]
        public async Task<StatsModel> Get()
        {
            try
            {
                var results = await _stats.GetStats();
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Get Stats", ex);
                throw;
            }
        }
    }
}
