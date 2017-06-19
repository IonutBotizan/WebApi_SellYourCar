using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SellYourCar.Controllers;
using SellYourCar.Entities;

namespace SellYourCar.ViewModels
{
    public class MakeCarUrlResolver : IValueResolver<MakeCar, MakeCarViewModel, string>
    {
        IHttpContextAccessor _accessor;
        public MakeCarUrlResolver(IHttpContextAccessor accessor)
        {
            _accessor=accessor;
        }
        public string Resolve(MakeCar source, MakeCarViewModel destination, string destMember, ResolutionContext context)
        {
            var url =(IUrlHelper)_accessor.HttpContext.Items[BaseController.urlHelper]; 
                 return url.Link("GetMake", new { makename= source.Name});
        }
    }
    public class ModelCarUrlResolver : IValueResolver<ModelCar , ModelCarViewModel , string>
    {
        IHttpContextAccessor _accessor;
        public ModelCarUrlResolver(IHttpContextAccessor accessor)
        {
            _accessor=accessor;
        }
        public string Resolve(ModelCar source, ModelCarViewModel destination, string destMember, ResolutionContext context)
        {
            var url =(IUrlHelper)_accessor.HttpContext.Items[BaseController.urlHelper]; 
                 return url.Link("GetTheModel", new { makename= source.Make.Name, modelname = source.Name.Trim()});
        }
    }

    public class AdvertiseUrlResolver : IValueResolver<Advertise, AdvertiseViewModel, string>
    {
        IHttpContextAccessor _accessor;
        public AdvertiseUrlResolver(IHttpContextAccessor accessor)
        {
            _accessor=accessor;
        }
        public string Resolve(Advertise source, AdvertiseViewModel destination, string destMember, ResolutionContext context)
        {
            var helper = (IUrlHelper)_accessor.HttpContext.Items[BaseController.urlHelper];
            return helper.Link("GetAdvertise", new {makename = source.ModelCar.Make.Name , modelname= source.ModelCar.Name, 
            id= source.Id});
        }
    }


}