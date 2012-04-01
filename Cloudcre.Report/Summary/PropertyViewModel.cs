using System;
using System.Collections.Generic;
using System.ComponentModel;
using AutoMapper;
using Cloudcre.Model;

namespace Cloudcre.Report.Summary
{
    public class PropertyViewModel
    {
        public PropertyViewModel()
        {
            Address = new AddressViewModel();
        }

        public enum PropertySaleStatus
        {
            None,
            Listed,
            UnderContract,
            Sold
        }

        public AddressViewModel Address { get; set; }

        public string Name { get; set; }

        public string ParcelId { get; set; }

        public decimal? SiteTotalSquareFoot { get; set; }

        public decimal? Acres { get; set; }

        public decimal? FloorToAreaRatio { get; set; }

        public decimal? BuildingAreaRatio { get; set; }

        public string Zoning { get; set; }

        public string Parking { get; set; }

        public string LegalDescription { get; set; }

        public string FloodZone { get; set; }

        public string CensusTract { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Price { get; set; }

        public DateTime? SaleDate { get; set; }

        public DateTime? ListingDate { get; set; }

        public DateTime? ContractDate { get; set; }

        public virtual PropertySaleStatus SaleStatus
        {
            get
            {
                if (SaleDate.HasValue)
                    return PropertySaleStatus.Sold;
                if (ContractDate.HasValue)
                    return PropertySaleStatus.UnderContract;
                if(ListingDate.HasValue)
                    return PropertySaleStatus.Listed;

                return PropertySaleStatus.None;
            }
        }

        [DisplayName("O.R. B/P")]
        public string OfficialRecordBookAndPage { get; set; }

        public string Grantor { get; set; }

        public string Grantee { get; set; }

        [DisplayName("Price/SF Bldg")]
        public decimal? CostPerBuildingSquareFoot { get; set; }

        [DisplayName("Price/Unit")]
        public decimal? CostPerUnit { get; set; }

        [DisplayName("Property Rights")]
        public string PropertyRights { get; set; }

        [DisplayName("Financing")]
        public string Financing { get; set; }

        [DisplayName("Listed Price at Sale")]
        public string ListedPriceAtSale { get; set; }

        [DisplayName("Marketing Period")]
        public string MarketingPeriod { get; set; }

        [DisplayName("Contract Period (Escrow)")]
        public string ContractPeriod { get; set; }

        public string Verification { get; set; }

        [DisplayName("Prior Sales")]
        public string PriorSales { get; set; }

        [DisplayName("Size")]
        public decimal? BuildingTotalSquareFoot { get; set; }

        [DisplayName("Year Built")]
        public string BuiltTimePeriod { get; set; }

        [DisplayName("Condition")]
        public string Condition { get; set; }

        [DisplayName("Class")]
        public string Class { get; set; }

        [DisplayName("Quality")]
        public string Quality { get; set; }

        public int? Stories { get; set; }

        public int? Units { get; set; }

        [DisplayName("Bedroom Mix")]
        public string BedroomMix { get; set; }

        [DisplayName("Avg. Unit Size")]
        public decimal? AverageSquareFootPerUnit { get; set; }

        public decimal? Occupancy { get; set; }

        [DisplayName("PGI")]
        public decimal? PotentialGrossIncome { get; set; }

        [DisplayName("EGI")]
        public decimal? EffectiveGrossIncome { get; set; }

        [DisplayName("Operating Expense")]
        public decimal? OperatingExpense { get; set; }

        [DisplayName("NOI")]
        public decimal? NetOperatingIncome { get; set; }

        [DisplayName("OAR (CAP)")]
        public decimal? OverallRate { get; set; }

        [DisplayName("GRM")]
        public decimal? GrossRentMultiplier { get; set; }
    }

    public static class PropertyMapper
    {
        public static IEnumerable<PropertyViewModel> ConvertToPropertyViews(this IEnumerable<MultipleFamily> obj)
        {
            return Mapper.Map<IEnumerable<MultipleFamily>, IEnumerable<PropertyViewModel>>(obj);
        }

        public static IEnumerable<MultipleFamily> ConvertToPropertyModels(this IEnumerable<PropertyViewModel> obj)
        {
            return Mapper.Map<IEnumerable<PropertyViewModel>, IEnumerable<MultipleFamily>>(obj);
        }

        public static PropertyViewModel ConvertToPropertyView(this MultipleFamily obj)
        {
            return Mapper.Map<MultipleFamily, PropertyViewModel>(obj);
        }

        public static MultipleFamily ConvertToPropertyModel(this PropertyViewModel obj)
        {
            return Mapper.Map<PropertyViewModel, MultipleFamily>(obj);
        }
    }
}