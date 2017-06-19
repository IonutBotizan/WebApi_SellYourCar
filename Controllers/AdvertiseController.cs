using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SellYourCar.Repos;
using SellYourCar.ViewModels;

namespace SellYourCar.Controllers
{

    [Route("api/make/{makename}/model/{modelname}")]
    public class AdvertiseController : BaseController
    {
        IMapper _mapper;
        IAdvertiseCarRepo _advertiseRepo;
        IModelCarRepo _modelCarRepo;
        const int page_size=2;
        public AdvertiseController(IAdvertiseCarRepo advertiseRepo, IMapper mapper, IModelCarRepo repo)
        {
            _mapper = mapper;
            _advertiseRepo = advertiseRepo;
            _modelCarRepo = repo;
        }
        [HttpGet("anunturi/page/{page:int=1}")]
        public IActionResult GetAll(string makename, string modelname,int? page, AdvertiseSearchViewModel search)
        {
            IEnumerable<Advertise> _advertises = null;
           int curentPage = page.Value;
            if (modelname.Equals("all_models"))
            {
                if (makename.Equals("all_makes"))
                {
                    _advertises = _advertiseRepo.AllIncluding(p => p.ModelCar, p => p.ModelCar.Make);

                }
                else
                {
                    _advertises = _advertiseRepo.GetAllAdsFromMake(makename);
                }
                 
 
            }
            else
            {
            _advertises = _advertiseRepo.GetAdvertisesByModelName(modelname);
            
            if (_advertises.Any(a => a.ModelCar.Make.Name != makename))
                return BadRequest("Model and make do not match");

            }
            _advertises= _advertises
                    .Where(a=>search.PriceFrom==null||a.Price>search.PriceFrom);
                    
            int totalAdds = _advertises.Count();
            double totalPages = Math.Ceiling((double)totalAdds/page_size);
           
           _advertises =_advertises.Skip(page_size*(curentPage-1)).Take(page_size).ToList();
                 
            return Ok(_mapper.Map<IEnumerable<AdvertiseViewModel>>(_advertises));

        }
        [HttpGet("anunt/{id}", Name = "GetAdvertise")]
        public IActionResult Get(string makename, string modelname, int id)
        {
            var _add = _advertiseRepo.GetAdvertiseById(id);
            if (_add.ModelCar.Name != modelname || _add.ModelCar.Make.Name != makename) return BadRequest("invalid add");
            return Ok(_mapper.Map<AdvertiseViewModel>(_add));
        }
        [HttpPost("anunt")]
        public async Task<IActionResult> Post(string makename, string modelname, [FromBody] AdvertiseViewModel model)
        {
            try
            {
                var modelCar = _modelCarRepo.GetModelByName(modelname);
                if (modelCar != null)
                {
                    var advertise = _mapper.Map<Advertise>(model);


                    advertise.ModelCar = modelCar;
                    advertise.DateAdded = DateTime.Now;
                    advertise.DateUpdated = DateTime.Now;

                    _advertiseRepo.Add(advertise);

                    if (await _advertiseRepo.SaveAllAsync())
                    {
                        return Created(Url.Link("GetAdvertise", new { makename = makename, modelname = modelname, id = advertise.Id }), 
                        _mapper.Map<AdvertiseViewModel>(advertise));
                    }
                }
            }
            catch
            {

            }

            return BadRequest("Failed to save new talk");

        }

        [HttpPut("anunt/{id}")]
        public async Task<IActionResult> Put(string makename, string modelname, int id , [FromBody]AdvertiseViewModel vm)
        {
            try
            {
                var advertise = _advertiseRepo.GetAdvertiseById(id);
                if(advertise==null) return NotFound();
                if(advertise.ModelCar.Name!=modelname) return BadRequest("Make and model dont match ");
                 vm.DateAdded = advertise.DateAdded;
                 vm.DateUpdated = DateTime.Now;
                _mapper.Map(vm , advertise);
                if(await _advertiseRepo.SaveAllAsync())
                {
                    return Ok(_mapper.Map<AdvertiseViewModel>(advertise));
                }
            }
            catch{}
            return BadRequest("Failed to update the add");
        }
        [HttpDelete("anunt/{id}")]
        public async Task<IActionResult> Delete(string makename , string modelname , int id)
        {
            try
            {
                var add = _advertiseRepo.GetAdvertiseById(id);
                if(add==null) return NotFound();
                if(add.ModelCar.Name!=modelname) return BadRequest("the make and model dont match");

                _advertiseRepo.Delete(add);
             if(await _advertiseRepo.SaveAllAsync())
             {
                 return Ok();
             }
            }
            catch {}
            return BadRequest("Could not delete the add");
        }
    }
}
