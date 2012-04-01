using System;
using System.Collections.Generic;
using System.ComponentModel;
using AutoMapper;
using Cloudcre.Model;

namespace Cloudcre.Report.Summary
{
    [Serializable]
    public class AddressViewModel
    {
        #region Address

        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        public string County { get; set; }

        [DisplayName("State, Province, or Region")]
        public string StateProvinceRegion { get; set; }

        [DisplayName("Zip")]
        public string Zip { get; set; }

        [DisplayName("MSA")]
        public string MetropolitanStatisticalArea { get; set; }

        #endregion
    }

    public static class AddressMapper
    {
        public static IEnumerable<AddressViewModel> ConvertToAddressViews(this IEnumerable<Address> obj)
        {
            return Mapper.Map<IEnumerable<Address>, IEnumerable<AddressViewModel>>(obj);
        }

        public static IEnumerable<Address> ConvertToAddressModels(this IEnumerable<AddressViewModel> obj)
        {
            return Mapper.Map<IEnumerable<AddressViewModel>, IEnumerable<Address>>(obj);
        }

        public static AddressViewModel ConvertToAddressView(this Address obj)
        {
            return Mapper.Map<Address, AddressViewModel>(obj);
        }

        public static Address ConvertToAddressModel(this AddressViewModel obj)
        {
            return Mapper.Map<AddressViewModel, Address>(obj);
        }
    }
}
