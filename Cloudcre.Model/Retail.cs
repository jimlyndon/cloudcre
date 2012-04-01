using System;

namespace Cloudcre.Model
{
    public class Retail : BuildingProperty
    {
        public virtual decimal? AverageAnnualDailyTraffic { get; set; }

        public virtual decimal? CostPerSiteSquareFoot
        {
            get
            {
                if (Price.HasValue && SiteTotalSquareFoot.HasValue)
                    return Price/SiteTotalSquareFoot;

                return null;
            }
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}