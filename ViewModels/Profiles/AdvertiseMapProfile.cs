using System;
using System.Linq;
using AutoMapper;
using SellYourCar.Entities;

namespace SellYourCar.ViewModels.Profiles
{
    public class AdvertiseMapProfile : Profile
    {
        public AdvertiseMapProfile()
        {
            CreateMap<Advertise, AdvertiseViewModel>()
            .ForMember(p => p.FullName, opt => opt.MapFrom(model => model.ModelCar.Make.Name + " " + model.ModelCar.Name))
                .ForMember(p => p.UserWhoAdded,
                opt => opt.MapFrom(model => model.UserWhoAdded))
                .ForMember(p => p.Url, opt => opt.ResolveUsing<AdvertiseUrlResolver>())
                
                .ForMember(p=>p.Fuel, opt=> opt.MapFrom(model=>((Fuel)model.Fuel).ToString()))
                .ForMember(p=>p.Fuels, opt=> opt.UseValue(Enum.GetNames(typeof(Fuel)).ToArray()))
                .ReverseMap();
        }

    }
}