using AutoMapper;
using SellYourCar.Entities;

namespace SellYourCar.ViewModels.Profiles
{
    public class ModelCarMapProfile : Profile
    {
        public ModelCarMapProfile()
        {
             CreateMap<ModelCar , ModelCarViewModel>()
               .ForMember(p=> p.FullName, opt=> opt.MapFrom(model => model.Make.Name + " "+ model.Name))
               .ForMember(p=> p.Url, opt=> opt.ResolveUsing<ModelCarUrlResolver>())
               .ReverseMap();
        }
    }
}