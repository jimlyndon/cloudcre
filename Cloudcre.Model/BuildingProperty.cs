namespace Cloudcre.Model
{
    public abstract class BuildingProperty : Property
    {
        public virtual decimal? BuildingAreaRatio { get; set; }

        public virtual decimal? BuildingTotalSquareFoot { get; set; }

        public virtual string BuiltTimePeriod { get; set; }

        public virtual decimal? CostPerBuildingSquareFoot
        {
            get
            {
                if (Price.HasValue && BuildingTotalSquareFoot.HasValue)
                    return Price / BuildingTotalSquareFoot;

                return null;
            }
        }

        public virtual decimal? EffectiveGrossIncomePerBuildingSquareFoot
        {
            get
            {
                if (EffectiveGrossIncome.HasValue && BuildingTotalSquareFoot.HasValue)
                    return (EffectiveGrossIncome / BuildingTotalSquareFoot);

                return null;
            }
        }

        public virtual decimal? FloorToAreaRatio
        {
            get
            {
                if (BuildingTotalSquareFoot.HasValue && SiteTotalSquareFoot.HasValue)
                    return BuildingTotalSquareFoot / SiteTotalSquareFoot;

                return null;
            }
        }

        public virtual decimal? NetOperatingIncomePerBuildingSquareFoot
        {
            get
            {
                if (NetOperatingIncome.HasValue && BuildingTotalSquareFoot.HasValue)
                    return (NetOperatingIncome / BuildingTotalSquareFoot);

                return null;
            }
        }

        public virtual decimal? OperatingExpensePerBuildingSquareFoot
        {
            get
            {
                if (OperatingExpense.HasValue && BuildingTotalSquareFoot.HasValue)
                    return (OperatingExpense / BuildingTotalSquareFoot);

                return null;
            }
        }

        public virtual decimal? OverallRate
        {
            get
            {
                if (NetOperatingIncome.HasValue && BuildingTotalSquareFoot.HasValue && CostPerBuildingSquareFoot.HasValue)
                    return ((NetOperatingIncome / BuildingTotalSquareFoot) / CostPerBuildingSquareFoot) * 100;

                return null;
            }
        }

        public virtual decimal? PotentialGrossIncomePerBuildingSquareFoot
        {
            get
            {
                if (PotentialGrossIncome.HasValue && BuildingTotalSquareFoot.HasValue)
                    return (PotentialGrossIncome / BuildingTotalSquareFoot);

                return null;
            }
        }
    }
}