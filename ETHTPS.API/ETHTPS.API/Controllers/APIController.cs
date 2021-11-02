﻿using ETHTPS.API.Infrastructure;
using ETHTPS.Data;
using ETHTPS.Data.Database;
using ETHTPS.Data.Database.Extensions;
using ETHTPS.Data.ResponseModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ETHTPS.API.Controllers
{
    [Route("API/[action]")]
    public class APIController : ControllerBase
    {
        private readonly ETHTPSContext _context;

        public APIController(ETHTPSContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Provider> Providers()
        {
            return _context.Providers;
        }

        [HttpGet]
        public IEnumerable<ProviderType> ProviderTypes()
        {
            return _context.ProviderTypes;
        }

        [HttpGet]
        public IEnumerable<Network> Networks()
        {
            return _context.Networks;
        }

        [HttpGet]
        public IEnumerable<string> Intervals()
        {
            foreach(var interval in Enum.GetValues(typeof(TimeInterval)))
            {
                if (interval.ToString() == "Instant" || interval.ToString() == "Latest")
                    continue;

                yield return interval.ToString();
            }
        }

        [HttpGet]
        public async Task<IEnumerable<TPSDataPoint>> GetTPS(string provider, string interval) => await _context.GetCachedResponseAsync<IEnumerable<TPSDataPoint>>(provider, interval);

    }

}
