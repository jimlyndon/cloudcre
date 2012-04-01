using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cloudcre.Model;
using Cloudcre.Service.Properties;

namespace Cloudcre.Service.ViewModels
{
    [Serializable]
    public class IndustrialViewModel : BuildingPropertyViewModel
    {
        [DisplayName("Construction Material")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public virtual string ConstructionMaterial { get; set; }

        [DisplayName("Price/SF Land")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? CostPerSiteSquareFoot { get; set; }

        [DisplayName("Office Finish %")]
        [RegularExpression(@"^(1[0][0])(\.[0]{1,2})?|([0-9]|[1-9][0-9])?(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgPercentageRange")]
        public virtual decimal? PercentOfBuildingFinished { get; set; }

        public override PropertyType PropertyType
        {
            get { return PropertyType.Industrial; }
        }

        [DisplayName("W/H Clear Height")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public virtual decimal? WareHouseClearHeight { get; set; }

        [DisplayName("W/H Access")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public virtual string WareHouseAccess { get; set; }
    }
}