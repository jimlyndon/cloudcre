using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cloudcre.Service.Properties;

namespace Cloudcre.Service.Property.ViewModels
{
    [Serializable]
    public abstract class BuildingPropertyViewModel : PropertyViewModel
    {
        [DisplayName("BAR")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public decimal? BuildingAreaRatio { get; set; }

        [DisplayName("Size")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public decimal? BuildingTotalSquareFoot { get; set; }

        [DisplayName("Year Built")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string BuiltTimePeriod { get; set; }

        [DisplayName("Price/SF Bldg")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? CostPerBuildingSquareFoot { get; set; }

        [DisplayName("FAR")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public decimal? FloorToAreaRatio { get; set; }

        [DisplayName("OAR (CAP)")]
        [RegularExpression(@"^(1[0][0])(\.[0]{1,2})?|([0-9]|[1-9][0-9])?(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgPercentageRange")]
        public decimal? OverallRate { get; set; }
    }
}
