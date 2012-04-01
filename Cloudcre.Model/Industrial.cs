using System;

namespace Cloudcre.Model
{
    public class Industrial : BuildingProperty
    {
        public virtual string ConstructionMaterial { get; set; }

        public virtual decimal? CostPerSiteSquareFoot
        {
            get
            {
                if (Price.HasValue && SiteTotalSquareFoot.HasValue)
                    return Price / SiteTotalSquareFoot;

                return null;
            }
        }

        public virtual decimal? PercentOfBuildingFinished { get; set; }

        public virtual string WareHouseAccess { get; set; }

        public virtual decimal? WareHouseClearHeight { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}