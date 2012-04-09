using System.Collections.Generic;
using AutoMapper;
using Cloudcre.Model;
using Cloudcre.Service.Property.ViewModels;

namespace Cloudcre.Service.Property.Mapping
{
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