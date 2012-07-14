using Cloudcre.Service.Property.ViewModels;

namespace Cloudcre.Service.Report.Summary
{
    public static class PropertyViewModelHelper
    {
        public enum PropertySaleStatus
        {
            None,
            Listed,
            UnderContract,
            Sold
        }

        public static PropertySaleStatus SaleStatus(this PropertyViewModel viewModel)
        {
                if (viewModel.SaleDate.HasValue)
                    return PropertySaleStatus.Sold;
                if (viewModel.ContractDate.HasValue)
                    return PropertySaleStatus.UnderContract;
                //return viewModel.ListingDate.HasValue ? PropertySaleStatus.Listed : PropertySaleStatus.None;
            return PropertySaleStatus.Listed;
        }

        public static decimal? Occupancy(this PropertyViewModel viewModel)
        {
            if (viewModel.Occupancy.HasValue)
                return viewModel.Occupancy/100M;

            return null;
        }

        public static string Comments(this PropertyViewModel viewModel)
        {
            var result = string.Empty;
            result += (string.IsNullOrWhiteSpace(viewModel.ListedPriceAtSale) ? string.Empty : "Listing: " + viewModel.ListedPriceAtSale + ".  ");
            result += (string.IsNullOrWhiteSpace(viewModel.ContractPeriod) ? string.Empty : "Escrow: " + viewModel.ContractPeriod + ".  ");
            result += (string.IsNullOrWhiteSpace(viewModel.Financing) ? string.Empty : "Financing: " + viewModel.Financing + ".  ");
            result += (string.IsNullOrWhiteSpace(viewModel.PriorSales) ? string.Empty : "Prior Sales: " + viewModel.PriorSales + ".  ");
            result += viewModel.Comments;
            return result;
        }
    }

    public static class BuildingPropertyViewModelHelper
    {
        public static decimal? OverallRate(this BuildingPropertyViewModel viewModel)
        {
            if (viewModel.OverallRate.HasValue)
                return viewModel.OverallRate / 100M;

            return null;
        }
    }

    public static class MultipleFamilyViewModelHelper
    {
        public static decimal? NetOperatingIncomePerUnitAverageSquareFoot(this MultipleFamilyViewModel viewModel)
        {
            if (viewModel.NetOperatingIncome.HasValue && viewModel.AverageSquareFootPerUnit.HasValue)
                return viewModel.NetOperatingIncome / viewModel.AverageSquareFootPerUnit;

            return null;
        }
    }
}