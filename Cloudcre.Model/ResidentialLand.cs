using System;

namespace Cloudcre.Model
{
    public class ResidentialLand : Property
    {
        public virtual string Approvals { get; set; }

        public virtual decimal? CostPerSiteSquareFoot
        {
            get
            {
                if (Price.HasValue && SiteTotalSquareFoot.HasValue)
                    return Price / SiteTotalSquareFoot;

                return null;
            }
        }

        public virtual decimal? CostPerUnit
        {
            get
            {
                if (Price.HasValue && Units.HasValue)
                    return (Price / Units);

                return null;
            }
        }

        public virtual decimal? Density { get; set; }

        public virtual decimal? EffectiveGrossIncomePerUnit
        {
            get
            {
                if (EffectiveGrossIncome.HasValue && Units.HasValue)
                    return (EffectiveGrossIncome / Units);

                return null;
            }
        }

        public virtual string FutureLandUse { get; set; }

        public virtual decimal? NetOperatingIncomePerUnit
        {
            get
            {
                if (NetOperatingIncome.HasValue && Units.HasValue)
                    return (NetOperatingIncome / Units);

                return null;
            }
        }

        public virtual decimal? OperatingExpensePerUnit
        {
            get
            {
                if (OperatingExpense.HasValue && Units.HasValue)
                    return (OperatingExpense / Units);

                return null;
            }
        }

        public virtual string PlannedUse { get; set; }

        public virtual decimal? PotentialGrossIncomePerUnit
        {
            get
            {
                if (PotentialGrossIncome.HasValue && Units.HasValue)
                    return (PotentialGrossIncome / Units);

                return null;
            }
        }

        public virtual string Surface { get; set; }

        public virtual string Utilities { get; set; }

        public virtual int? Units { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}