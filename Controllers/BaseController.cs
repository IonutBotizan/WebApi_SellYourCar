using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SellYourCar.Controllers
{
    public class BaseController: Controller
    {
        public const string urlHelper = "URLHELPER";
       
       public override void OnActionExecuting(ActionExecutingContext context)
        {
             base.OnActionExecuting(context);
             context.HttpContext.Items[urlHelper] = this.Url;
             

        }
    }
}