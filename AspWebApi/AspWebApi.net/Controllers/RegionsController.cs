using Microsoft.AspNetCore.Mvc;
using AspWebApi.net.Models.Domain;
using AspWebApi.net.Repositories;
using AutoMapper;
using AspWebApi.net.Models.DTO;

namespace AspWebApi.net.Controllers
{
    [ApiController]
    //[Route("Regions")]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllResults()
        {
            var regions=await regionRepository.GetAllAsync();

            //return DTO region
            //var regionsDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };
            //    regionsDTO.Add(regionDTO);
            //});
            
            var regionsDTO=mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO=mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //Request to domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
            };

            //pass details to repo
            var response=await regionRepository.AddAsync(region);

            //convert back to dto
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get region from database
            var region=await regionRepository.DeleteAsync(id);
            //if null notfound
            if (region == null)
            {
                return NotFound();
            }

            //convert response back to dto
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };
            //return of response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id,[FromBody]UpdateRegionRequest updateRegionRequest)
        {
            //convert dto domain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Area = updateRegionRequest.Area,
                Population = updateRegionRequest.Population,
            };
            //update region using repository
            var uregion = await regionRepository.UpdateAsync(id, region);
            //if null then notfound
            if(uregion == null)
            {
                return NotFound();
            }
            //convert domain back to dto
            var regionDTO = new Models.DTO.Region()
            {
                Id = uregion.Id,
                Name = uregion.Name,
                Code = uregion.Code,
                Long = uregion.Long,
                Lat = uregion.Lat,
                Population = uregion.Population,
                Area = uregion.Area,
            };

            //return ok response
            return Ok(regionDTO);
        }
    }
}
