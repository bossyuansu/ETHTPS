﻿using ETHTPS.API.Infrastructure.Database.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ETHTPS.TPSLogger.TPSLogging.ScanLogger
{
    public class EtherscanTPSLogger : ScanTPSLoggerBase
    {
        public EtherscanTPSLogger(ETHTPSContext context, string name, string apiKey) : base(context, name, apiKey, "etherscan", 13.1)
        {
        }
    }
}
