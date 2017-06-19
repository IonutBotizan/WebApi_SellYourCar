using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SellYourCar.Entities;
using SellYourCar.Repos;
using SellYourCar.ViewModels;

namespace SellYourCar.Controllers
{
    [Route("api/make")]
    public class MakeCarController : BaseController
    {
        IMakeCarRepo _makeRepo ; 
        IMapper _mapper;
        public MakeCarController(IMakeCarRepo repo , IMapper mapper)
        {
            _makeRepo = repo;
            _mapper = mapper;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
          var makes = _makeRepo.GetAll();
          return Ok(_mapper.Map<IEnumerable<MakeCarViewModel>>(makes));
        }
        [HttpGet("{makename}", Name="GetMake")]
        public IActionResult Get(string makename)
        {
        try
        {
            MakeCar make = _makeRepo.GetMakeCarByMoniker(makename);
            if(make==null) return NotFound($"Make {makename} not found");
            return Ok(_mapper.Map<MakeCarViewModel>(make));
        }
        catch{}
        return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MakeCarViewModel vm)
        {
            //instead of a global filter I am using FV so this 
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               var makeEntity = _mapper.Map<MakeCar>(vm);

               _makeRepo.Add(makeEntity);
               if(await _makeRepo.SaveAllAsync())
               {
                   var newUri = Url.Link("GetMake", new {makename= makeEntity.Name});
                   return Created(newUri ,_mapper.Map<MakeCarViewModel>(makeEntity));
               }
            }
            catch
            {}
            return BadRequest();
        }

        [HttpPut("{makename}")]
        public async Task<IActionResult> Put(string makename , [FromBody]MakeCar make)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var oldMake = _makeRepo.GetMakeCarByMoniker(makename);
                if(oldMake==null) return NotFound();
               _makeRepo.Update(oldMake);
                //map model to the oldmodel
                 _mapper.Map(make , oldMake); 
                  
                if(await _makeRepo.SaveAllAsync())
                {
                    return Ok(_mapper.Map<MakeCarViewModel>(oldMake));
                }



            }
            catch{}
            return BadRequest("Couldn't update the make");
        }
        [HttpDelete("{makename}")]
        public async Task<IActionResult> Delete(string makename)
        {
            try
            {
                var oldMake = _makeRepo.GetMakeCarByMoniker(makename);
                if(oldMake==null) return NotFound();
                _makeRepo.Delete(oldMake);
                if(await _makeRepo.SaveAllAsync())
                {
                    return Ok();
                }

            } 
            catch
            {

            }
            return BadRequest("couldnt delete");
        }

         
    }
}