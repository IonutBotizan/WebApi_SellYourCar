using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SellYourCar.Entities;
using SellYourCar.Repos;
using SellYourCar.ViewModels;

namespace SellYourCar.Controllers
{
    [Route("api/make/{makename}/model")]
    public class ModelCarController : BaseController
    {
        IMapper _mapper;
        IModelCarRepo _modelRepo; 
        IMakeCarRepo _makeRepo;
        public ModelCarController(IMapper mapper , IModelCarRepo modelRepo, IMakeCarRepo makeRepo)
        {
            _mapper = mapper;
            _modelRepo = modelRepo; 
            _makeRepo= makeRepo;
        }
        [HttpGet]
        public IActionResult Get(string makename)
        {
            var models = _modelRepo.GetModelsByMake(makename);
           return Ok(_mapper.Map<IEnumerable<ModelCarViewModel>>(models));
        }
        [HttpGet("{modelname}", Name="GetTheModel")]
        public IActionResult Get(string makename , string modelname)
        {
            ModelCar model = _modelRepo.GetModelByName(modelname);
            if(model==null) return NotFound();
            if(model.Make.Name!=makename) return BadRequest("there is no model for this make");
            return Ok(_mapper.Map<ModelCarViewModel>(model)); 
        }
        [HttpPost]
        public async Task<IActionResult> Post(string makename, [FromBody]ModelCarViewModel model)
        {
            try
            {
                   var makeCar = _makeRepo.GetMakeCarByMoniker(makename);             
                    if(makeCar==null)
                    return BadRequest("There is no make");
                    
                    var modelFromBody = _mapper.Map<ModelCar>(model);
                    modelFromBody.Make = makeCar;
                _modelRepo.Add(modelFromBody);
                 if(await _modelRepo.SaveAllAsync())
                 {
                     var url = Url.Link("GetTheModel", new {makename = makeCar.Name , modelname = modelFromBody.Name});
                     return Created(url , _mapper.Map<ModelCarViewModel>(modelFromBody));
                 }
            }
            catch
            {

            }
            return BadRequest("couldnt create a mdoel ");
        }

        [HttpPut("{modelname}")]
        public async Task<IActionResult> Put(string makename,string modelname , [FromBody]ModelCarViewModel model)
        {
              try
              {
                  var modelEntity = _modelRepo.GetModelByName(modelname);
                  if(modelEntity == null) return NotFound();
                  if(modelEntity.Make.Name != makename) return BadRequest("there is no model for that make");

                  _mapper.Map(model, modelEntity );

                  if(await _modelRepo.SaveAllAsync())
                  {
                      return Ok(_mapper.Map<ModelCarViewModel>(modelEntity));
                  }

              }
              catch{}
              return BadRequest("Could not update Model");
        }
    [HttpDelete("{modelname}")]
    public async Task<IActionResult> Delete(string makename, string modelname)
    {	
      try
      {
        var modelEntity = _modelRepo.GetModelByName(modelname);
        if (modelEntity == null) return NotFound();
        if (modelEntity.Make.Name != makename) return BadRequest("Model and Make do not match");


        _modelRepo.Delete(modelEntity);

        if (await _modelRepo.SaveAllAsync())
        {
          return Ok();
        }
      }
      catch 
      {
      }

      return BadRequest("Could not delete speaker");
    }
    }
}