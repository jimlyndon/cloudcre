using System;

namespace Cloudcre.Model
{
    public class MultipleFamily : BuildingProperty
    {
        public virtual decimal? AverageSquareFootPerUnit
        {
            get
            {
                if (BuildingTotalSquareFoot.HasValue && Units.HasValue)
                    return (BuildingTotalSquareFoot / Units);

                return null;
            }
        }

        public virtual string BedroomMix { get; set; }

        public virtual decimal? CostPerUnit
        {
            get
            {
                if (Price.HasValue && Units.HasValue)
                    return (Price / Units);

                return null;
            }
        }

        public virtual decimal? EffectiveGrossIncomePerUnit
        {
            get
            {
                if (EffectiveGrossIncome.HasValue && Units.HasValue)
                    return (EffectiveGrossIncome / Units);

                return null;
            }
        }

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

        public virtual decimal? PotentialGrossIncomePerUnit
        {
            get
            {
                if (PotentialGrossIncome.HasValue && Units.HasValue)
                    return (PotentialGrossIncome / Units);

                return null;
            }
        }

        public virtual int? Units { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}