using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cloudcre.Model;
using Cloudcre.Service.Properties;

namespace Cloudcre.Service.ViewModels
{
    [Serializable]
    public class RetailViewModel : BuildingPropertyViewModel
    {
        [DisplayName("AADT")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidNumber")]
        public decimal? AverageAnnualDailyTraffic { get; set; }

        [DisplayName("Price/SF Land")]
        [DataType(DataType.Currency, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgValidValue")]
        public decimal? CostPerSiteSquareFoot { get; set; }

        public override PropertyType PropertyType
        {
            get { return PropertyType.Retail; }
        }
    }
}