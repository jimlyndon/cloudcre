using System;

namespace Cloudcre.Model
{
    public class CommercialLand : BuildingProperty
    {
        public virtual string Approvals { get; set; }

        public virtual decimal? AverageAnnualDailyTraffic { get; set; }

        public virtual decimal? CostPerSiteSquareFoot
        {
            get
            {
                if (Price.HasValue && SiteTotalSquareFoot.HasValue)
                    return Price / SiteTotalSquareFoot;

                return null;
            }
        }

        public virtual decimal? FrontageToDepth { get; set; }

        public virtual string FrontFt { get; set; }

        public virtual string FutureLandUse { get; set; }

        public virtual string PlannedUse { get; set; }

        public virtual string ProposedSize { get; set; }

        public virtual string Surface { get; set; }

        public virtual string Utilities { get; set; }

        public virtual string Visibility { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}