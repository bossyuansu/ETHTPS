﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHTPS.Data.ResponseModels.HomePage
{
    public class HomePageViewModel
    {
        public IEnumerable<TPSResponseModel> InstantTPS { get; set; }
        public IEnumerable<ProviderInfo> ProviderData { get; set; }
        public Dictionary<string,string> ColorDictionary { get; set; }
        public Dictionary<string, Dictionary<string, IEnumerable<TPSResponseModel>>> TPSData { get; set; }
    }
}
