using ProductManager.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services.Interfaces
{
    public interface IStatsService
    {
        Task<StatsModel> GetStats();
    }
}
