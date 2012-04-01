using System;

namespace Cloudcre.Model
{
    public class CommercialCondominium : BuildingProperty
    {
        public virtual string SingleUnitCondition { get; set; }

        public virtual string SingleUnitFinish { get; set; }

        public virtual string SingleUnitFloor { get; set; }

        public virtual decimal? SingleUnitSquareFoot { get; set; }

        public virtual string SingleUnitUse { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}