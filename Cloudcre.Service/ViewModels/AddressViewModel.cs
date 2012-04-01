using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cloudcre.Service.Properties;

namespace Cloudcre.Service.ViewModels
{
    [Serializable]
    public class AddressViewModel
    {
        #region Address

        [DisplayName("Address Line 1")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange100")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address Line 2")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange100")]
        public string AddressLine2 { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string City { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string County { get; set; }

        [DisplayName("State, Province, or Region")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string StateProvinceRegion { get; set; }

        [DisplayName("Zip")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgFormatZip")]
        public string Zip { get; set; }

        [DisplayName("MSA")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ValidationMsgCharacterRange50")]
        public string MetropolitanStatisticalArea { get; set; }

        #endregion
    }
}
