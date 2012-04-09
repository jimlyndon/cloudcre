using System.Web.Mvc;
using AutoMapper;
using Cloudcre.Service.Property.Messages;
using Cloudcre.Service.Property.ViewModels;
using Cloudcre.Web.Models;

namespace Cloudcre.Web.Mapping
{
    internal class BootStrapper
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.CreateMap<MapBoundary, SearchPropertyRequest.MapBoundary>();
            Mapper.CreateMap<LngLatBoundary, SearchPropertyRequest.LngLatBoundary>();
            Mapper.CreateMap<Location, SearchPropertyRequest.Location>();

            //Mapper.CreateMap<AddressCountryType, SelectListItem>()
            //    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.GetDescription()))
            //    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (int)src));
            //Mapper.CreateMap<PropertyType, SelectListItem>()
            //    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.GetDescription()))
            //    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (int)src));
            //Mapper.CreateMap<Property.ParkingType, SelectListItem>()
            //    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.GetDescription()))
            //    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (int)src));

            Mapper.CreateMap<LocationViewModel, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.label))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.category));
        }
    }

    //public static class EnumExtensions
    //{
    //    public static string GetDescription(this Enum enumValue)
    //    {
    //        Type enumType = enumValue.GetType();

    //        FieldInfo fieldInfo = enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public)[
    //            (int) Convert.ChangeType(enumValue, typeof (int))];
            
    //        string display = enumValue.ToString();

    //        foreach (var valueAttribute in fieldInfo.GetCustomAttributes(true).OfType<EnumValueDataAttribute>())
    //        {
    //            display = valueAttribute.Name;
    //        }

    //        return display;
    //    }
    //}
}