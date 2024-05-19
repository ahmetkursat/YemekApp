using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.FeatureDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        private readonly IMapper _mapper;

        public FeatureController(IFeatureService featureService, IMapper mapper)
        {
            _featureService = featureService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult FeatureGet() {

            var list = _mapper.Map<List<ResultFeatureDto>>(_featureService.TGetListAll());
            return Ok(list);
        }

        [HttpPost]
        public IActionResult FeaturePost(CreateFeatureDto createFeatureDto)
        {
            Feature feature = new Feature()
            {
                FeatureID = createFeatureDto.FeatureID,
                Description1 = createFeatureDto.Description1,
                Description2 = createFeatureDto.Description2,
                Description3 = createFeatureDto.Description3,
                Title1 = createFeatureDto.Title1,
                Title2 = createFeatureDto.Title2,
                Title3 = createFeatureDto.Title3,
            };

            _featureService.TAdd(feature);
            return Ok(feature);
        }
        [HttpPut]
        public IActionResult FeatureUpdate (UpdateFeatureDto updateFeatureDto)
        {
            Feature feature = new Feature()
            {
                FeatureID = updateFeatureDto.FeatureID,
                Description1 = updateFeatureDto.Description1,
                Description2 = updateFeatureDto.Description2,
                Description3 = updateFeatureDto.Description3,
                Title1 = updateFeatureDto.Title1,
                Title2 = updateFeatureDto.Title2,
                Title3 = updateFeatureDto.Title3,
            };
            _featureService.TUpdate(feature);
            return Ok(feature);
        }
        [HttpDelete("{id}")]
        public IActionResult FeatureDelete (int id)
        {
            var list = _featureService.TGetById(id);
            _featureService.TDelete(list);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult FeatureGetId(int id)
        {
            var list = _featureService.TGetById(id);
            return Ok(list);
        }
        
    }
}
