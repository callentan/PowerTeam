using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerTeam.WebApps.WebMVC
{
    public class AppSettings
    {
        public string MarketingUrl { get; set; }
        public string PurchaseUrl { get; set; }
        public string SignalrHubUrl { get; set; }
        public bool ActivateCampaignDetailFunction { get; set; }
        public bool UseCustomizationData { get; set; }
    }
}
