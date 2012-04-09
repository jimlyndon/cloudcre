using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Cloudcre.Model;
using Cloudcre.Service.Properties;

namespace Cloudcre.Service.Property.ViewModels
{
    [Serializable]
    public class PropertyViewModel
    {
        [NonSerialized]
        private string _serializedViewModel;

        [XmlIgnore]
        public string serializedViewModel
        {
            get { return _serializedViewModel ?? (_serializedViewModel = String.Empty); }

            set { _serializedViewModel = value; }
        }

        public PropertyViewModel()
        {
            Address = new AddressViewModel();
        }

        public Guid Id { get; set; }

        [DisplayName("Acres")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public decimal? Acres { get; set; }

        public AddressViewModel Address { get; set; }

        [DisplayName("Census Tract")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string CensusTract { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange20")]
        public string Class { get; set; }

        [StringLength(1000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange1000")]
        [DataType(DataType.MultilineText)]
        public virtual string Comments { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange20")]
        public string Condition { get; set; }

        [DisplayName("Contract Date")]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidDate")]
        public DateTime? ContractDate { get; set; }

        [DisplayName("Contract Period (Escrow)")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange100")]
        public string ContractPeriod { get; set; }

        [DisplayName("EGI")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? EffectiveGrossIncome { get; set; }

        [DisplayName("Financing")]
        [StringLength(200, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange200")]
        public string Financing { get; set; }

        [DisplayName("Flood Zone")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string FloodZone { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange100")]
        public string Grantee { get; set; }

        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange100")]
        public string Grantor { get; set; }

        [DisplayName("GRM")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? GrossRentMultiplier { get; set; }

        [RegularExpression(@"^-?[0-9]+(\.[0-9]{1,50})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public decimal? Latitude { get; set; }

        [RegularExpression(@"^-?[0-9]+(\.[0-9]{1,50})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public decimal? Longitude { get; set; }

        [DisplayName("Legal Description")]
        [StringLength(150, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string LegalDescription { get; set; }

        [DisplayName("Listed Price at Sale")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange100")]
        public string ListedPriceAtSale { get; set; }

        [DisplayName("Listing Date")]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidDate")]
        public DateTime? ListingDate { get; set; }

        [DisplayName("Marketing Period")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange100")]
        public string MarketingPeriod { get; set; }

        [DisplayName("Property Name")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange100")]
        public string Name { get; set; }

        [DisplayName("NOI")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? NetOperatingIncome { get; set; }

        [RegularExpression(@"^(1[0][0])(\.[0]{1,2})?|([0-9]|[1-9][0-9])?(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgPercentageRange")]
        public decimal? Occupancy { get; set; }

        [DisplayName("O.R. B/P")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string OfficialRecordBookAndPage { get; set; }

        [DisplayName("Operating Expense")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? OperatingExpense { get; set; }

        [DisplayName("Parcel Id")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string ParcelId { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange20")]
        public string Parking { get; set; }

        [DisplayName("PGI")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? PotentialGrossIncome { get; set; }

        [DisplayName("Price")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? Price { get; set; }

        [DisplayName("Prior Sales")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange100")]
        public string PriorSales { get; set; }

        [DisplayName("Property Rights")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange100")]
        public string PropertyRights { get; set; }

        [DisplayName("Property Type")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgRequired")]
        public virtual PropertyType PropertyType { get; set; }

        public string PropertyTypeDescription { get { return PropertyType.ToString(); } }

        [StringLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange20")]
        public string Quality { get; set; }

        [DisplayName("Sale Date")]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidDate")]
        public DateTime? SaleDate { get; set; }

        [DisplayName("Size SF")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public decimal? SiteTotalSquareFoot { get; set; }

        [RegularExpression(@"^(\d+)$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public int? Stories { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string Use { get; set; }

        [StringLength(200, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange200")]
        public string Verification { get; set; }

        [StringLength(10, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange10")]
        public string Zoning { get; set; }
    }
}