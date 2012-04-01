using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cloudcre.Model;
using Cloudcre.Service.Properties;

namespace Cloudcre.Service.ViewModels
{
    [Serializable]
    public class IndustrialCondominiumViewModel : BuildingPropertyViewModel
    {
        [DisplayName("Finish %")]
        [RegularExpression(@"^(1[0][0])(\.[0]{1,2})?|([0-9]|[1-9][0-9])?(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgPercentageRange")]
        public virtual decimal? SingleUnitFinish { get; set; }

        [DisplayName("Condition")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange20")]
        public virtual string SingleUnitCondition { get; set; }

        [DisplayName("Square Foot")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public virtual decimal? SingleUnitSquareFoot { get; set; }

        [DisplayName("Use")]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange20")]
        public virtual string SingleUnitUse { get; set; }

        [DisplayName("W/H Clear Height")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public virtual decimal? WareHouseClearHeight { get; set; }

        [DisplayName("W/H Access")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public virtual string WareHouseAccess { get; set; }

        public override PropertyType PropertyType
        {
            get { return PropertyType.IndustrialCondominium; }
        }
    }
}