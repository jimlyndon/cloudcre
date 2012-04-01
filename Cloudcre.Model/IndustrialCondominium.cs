using System;

namespace Cloudcre.Model
{
    public class IndustrialCondominium : BuildingProperty
    {
        public virtual string SingleUnitCondition { get; set; }

        public virtual decimal? SingleUnitFinish { get; set; }

        public virtual decimal? SingleUnitSquareFoot { get; set; }

        public virtual string SingleUnitUse { get; set; }

        public virtual string WareHouseAccess { get; set; }

        public virtual decimal? WareHouseClearHeight { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}