using AutoMapper;
using Cloudcre.Model;
using Cloudcre.Service.Property.ViewModels;

namespace Cloudcre.Service.Property.Mapping
{
    public class BootStrapper
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.CreateMap<Address, AddressViewModel>();
            Mapper.CreateMap<AddressViewModel, Address>();

            Mapper.CreateMap<MultipleFamily, MultipleFamilyViewModel>();
            Mapper.CreateMap<MultipleFamilyViewModel, MultipleFamily>();

            Mapper.CreateMap<Office, OfficeViewModel>();
            Mapper.CreateMap<OfficeViewModel, Office>();

            Mapper.CreateMap<Retail, RetailViewModel>();
            Mapper.CreateMap<RetailViewModel, Retail>();

            Mapper.CreateMap<Industrial, IndustrialViewModel>();
            Mapper.CreateMap<IndustrialViewModel, Industrial>();

            Mapper.CreateMap<CommercialCondominium, CommercialCondominiumViewModel>();
            Mapper.CreateMap<CommercialCondominiumViewModel, CommercialCondominium>();

            Mapper.CreateMap<IndustrialCondominium, IndustrialCondominiumViewModel>();
            Mapper.CreateMap<IndustrialCondominiumViewModel, IndustrialCondominium>();

            Mapper.CreateMap<CommercialLand, CommercialLandViewModel>();
            Mapper.CreateMap<CommercialLandViewModel, CommercialLand>();

            Mapper.CreateMap<IndustrialLand, IndustrialLandViewModel>();
            Mapper.CreateMap<IndustrialLandViewModel, IndustrialLand>();

            Mapper.CreateMap<ResidentialLand, ResidentialLandViewModel>();
            Mapper.CreateMap<ResidentialLandViewModel, ResidentialLand>();
        }
    }
}