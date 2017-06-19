using AutoMapper;
using SellYourCar.Entities;

namespace SellYourCar.ViewModels.Profiles
{
       public class MakeCarMapProfile : Profile
    {
        public MakeCarMapProfile()
        {
            CreateMap<MakeCar , MakeCarViewModel>()
            .ForMember(m=> m.Url,opt=> opt.ResolveUsing<MakeCarUrlResolver>())
            .ReverseMap();
        }
    }

}