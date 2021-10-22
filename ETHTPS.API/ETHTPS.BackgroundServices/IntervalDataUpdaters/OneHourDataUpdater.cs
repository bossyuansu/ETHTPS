﻿

using ETHTPS.Data;
using ETHTPS.Data.Database;
using ETHTPS.Data.ResponseModels;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETHTPS.BackgroundServices.IntervalDataUpdaters
{
    public class OneHourDataUpdater : IntervalDataUpdaterBase
    {
        public OneHourDataUpdater(ILogger<BackgroundServiceBase> logger, IServiceScopeFactory serviceScopeFactory) : base(logger, serviceScopeFactory, "OneHour", TimeSpan.FromMinutes(5))
        {
        }

        public override Task<IEnumerable<TPSResponseModel>> RunAsync(ETHTPSContext context, int providerID, List<TPSResponseModel> currentCachedResponse)
        {
            var entries = context.Tpsdata.AsEnumerable().Where(x => x.Provider.Value == providerID && x.Date >= DateTime.Now.Subtract(TimeSpan.FromHours(1))).OrderBy(x => x.Date);
            var groups = entries.GroupBy(x => x.Date.Value.Minute);
            var list = new List<TPSResponseModel>();
            foreach (var group in groups)
            {
                list.Add(new TPSResponseModel()
                {
                    Date = group.First().Date.Value.Subtract(TimeSpan.FromSeconds(group.First().Date.Value.Second)).Subtract(TimeSpan.FromMilliseconds(group.First().Date.Value.Millisecond)).Subtract(TimeSpan.FromMilliseconds(group.First().Date.Value.Millisecond)),
                    TPS = group.Average(x => x.Tps.Value)
                });
            }
            var result = list.AsEnumerable();
            return Task.FromResult(result);
        }
    }
}
