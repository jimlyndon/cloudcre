using AutoMapper;
using Cloudcre.Model;

namespace Cloudcre.Report.Summary
{
    public class AutoMapperBootStrapper
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.CreateMap<Address, AddressViewModel>();
            Mapper.CreateMap<AddressViewModel, Address>();

            Mapper.CreateMap<MultipleFamily, PropertyViewModel>();
            Mapper.CreateMap<PropertyViewModel, MultipleFamily>();
        }
    }
}
