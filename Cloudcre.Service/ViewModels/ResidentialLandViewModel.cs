using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cloudcre.Model;
using Cloudcre.Service.Properties;

namespace Cloudcre.Service.ViewModels
{
    [Serializable]
    public class ResidentialLandViewModel : PropertyViewModel
    {
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string Approvals { get; set; }

        [DisplayName("Price/SF Land")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? CostPerSiteSquareFoot { get; set; }

        [DisplayName("Price/Unit")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? CostPerUnit { get; set; }

        [DisplayName("Density")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? Density { get; set; }

        [DisplayName("FLU")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string FutureLandUse { get; set; }

        [DisplayName("Planned Use")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string PlannedUse { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string Surface { get; set; }

        [RegularExpression(@"^(\d+)$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public int? Units { get; set; }

        [StringLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange20")]
        public string Utilities { get; set; }

        public override PropertyType PropertyType
        {
            get { return PropertyType.ResidentialLand; }
        }
    }
}