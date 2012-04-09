using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cloudcre.Model;
using Cloudcre.Service.Properties;

namespace Cloudcre.Service.Property.ViewModels
{
    [Serializable]
    public class CommercialLandViewModel : BuildingPropertyViewModel
    {
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string Approvals { get; set; }

        [DisplayName("AADT")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public decimal? AverageAnnualDailyTraffic { get; set; }

        [DisplayName("Price/SF Land")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? CostPerSiteSquareFoot { get; set; }

        [DisplayName("Frn / Depth")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string FrontageToDepth { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange20")]
        public string FrontFt { get; set; }

        [DisplayName("FLU")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string FutureLandUse { get; set; }

        [DisplayName("Planned Use")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string PlannedUse { get; set; }

        [DisplayName("Proposed Size")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange20")]
        public string ProposedSize { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string Surface { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange20")]
        public string Utilities { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string Visibility { get; set; }

        public override PropertyType PropertyType
        {
            get { return PropertyType.CommercialLand; }
        }
    }
}