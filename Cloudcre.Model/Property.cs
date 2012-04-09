using System;
using System.ComponentModel;
using Cloudcre.Model.Core;
using NHibernate.Search.Attributes;

namespace Cloudcre.Model
{
    [Flags]
    public enum PropertyType
    {
        [DescriptionAttribute("Multiple Family")]
        MultipleFamily = 1,
        [DescriptionAttribute("Office")]
        Office = 2,
        [DescriptionAttribute("Retail")]
        Retail = 4,
        [DescriptionAttribute("Industrial")]
        Industrial = 8,
        [DescriptionAttribute("Industrial Condominium")]
        IndustrialCondominium = 16,
        [DescriptionAttribute("Commercial Condominium")]
        CommercialCondominium = 32,
        [DescriptionAttribute("Commercial Land")]
        CommercialLand = 64,
        [DescriptionAttribute("Industrial Land")]
        IndustrialLand = 128,
        [DescriptionAttribute("Residential Land")]
        ResidentialLand = 256
    }

    [Indexed]
    public class Property : EntityBase<Guid>, IAggregateRoot
    {
        //public enum ParkingType
        //{
        //    [Description("Not Applicable")]
        //    NotApplicable = 0,

        //    Good = 1,
        //    Adequate = 2,
        //    Limited = 3,
        //    None = 4
        //}

        public virtual decimal? Acres
        {
            get
            {
                if (SiteTotalSquareFoot.HasValue)
                    return (SiteTotalSquareFoot / 43560M);
                
                return null;
            }
        }

        [IndexedEmbedded(Depth = 1)]
        public virtual Address Address { get; set; }

        [Field(Index.Tokenized)]
        public virtual string CensusTract { get; set; }

        public virtual string Class { get; set; }

        //public virtual IList<Comment> Comments { get; set; }
        public virtual string Comments { get; set; }

        public virtual string Condition { get; set; }

        public virtual DateTime? ContractDate { get; set; }

        public virtual string ContractPeriod { get; set; }

        public virtual decimal? EffectiveGrossIncome { get; set; }

        public virtual string Financing { get; set; }

        [Field(Index.Tokenized)]
        public virtual string FloodZone { get; set; }

        [Field(Index.Tokenized)]
        public virtual string Grantee { get; set; }

        [Field(Index.Tokenized)]
        public virtual string Grantor { get; set; }

        public virtual decimal? GrossRentMultiplier { get; set; }

        public virtual decimal? Latitude { get; set; }

        [Field(Index.Tokenized)]
        public virtual string LegalDescription { get; set; }

        public virtual string ListedPriceAtSale { get; set; }

        public virtual DateTime? ListingDate { get; set; }

        public virtual decimal? Longitude { get; set; }

        public virtual string MarketingPeriod { get; set; }

        [Field(Index.Tokenized)]
        public virtual string Name { get; set; }

        public virtual decimal? NetOperatingIncome { get; set; }

        public virtual decimal? Occupancy { get; set; }

        public virtual string OfficialRecordBookAndPage { get; set; }

        public virtual decimal? OperatingExpense { get; set; }

        [Field(Index.Tokenized)]
        public virtual string ParcelId { get; set; }

        public virtual string Parking { get; set; }

        public virtual decimal? PotentialGrossIncome { get; set; }

        public virtual decimal? Price { get; set; }

        public virtual string PriorSales { get; set; }

        public virtual string PropertyRights { get; set; }

        public virtual PropertyType PropertyType { get; set; }

        public virtual string Quality { get; set; }

        public virtual DateTime? SaleDate { get; set; }

        public virtual decimal? SiteTotalSquareFoot { get; set; }

        public virtual int? Stories { get; set; }

        public virtual string Use { get; set; }

        public virtual string Verification { get; set; }

        public virtual string Zoning { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}