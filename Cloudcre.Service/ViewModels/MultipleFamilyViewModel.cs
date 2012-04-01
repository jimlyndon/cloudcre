using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cloudcre.Model;
using Cloudcre.Service.Properties;

namespace Cloudcre.Service.ViewModels
{
    [Serializable]
    public class MultipleFamilyViewModel : BuildingPropertyViewModel
    {
        [DisplayName("Avg. Unit Size")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public decimal? AverageSquareFootPerUnit { get; set; }

        [DisplayName("Bedroom Mix")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string BedroomMix { get; set; }

        [DisplayName("Price/Unit")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? CostPerUnit { get; set; }

        public override PropertyType PropertyType
        {
            get { return PropertyType.MultipleFamily; }
        }

        [RegularExpression(@"^(\d+)$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public int? Units { get; set; }
    }
}