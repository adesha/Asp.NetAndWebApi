using AspWebApi.net.Models.DTO;
using AspWebApi.net.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspWebApi.net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResultsAsync()
        {
            var walks = await walkRepository.GelAllAsync();

            var walkDTO=mapper.Map<List<Models.DTO.Walk>>(walks);
            return Ok(walkDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walkRepository.GetWalkAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            walk = await walkRepository.AddAsync(walk);

            var walkDTO = new Models.DTO.Walk
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute]Guid id, [FromBody]UpdateWalkRequest updatewalk)
        {
            var walk = new Models.Domain.Walk()
            {
                Length = updatewalk.Length,
                Name = updatewalk.Name,
                RegionId = updatewalk.RegionId,
                WalkDifficultyId = updatewalk.WalkDifficultyId
            };
            var uwalk = await walkRepository.UpdateAsync(id, walk);
            if (uwalk == null)
            {
                return NotFound();
            }
            var walkDTO = new Models.DTO.Walk
            {
                Id = uwalk.Id,
                WalkDifficultyId = uwalk.WalkDifficultyId,
                Name = uwalk.Name,
                Length = uwalk.Length,
                RegionId = uwalk.RegionId
            };
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);
            if (walk == null)
            {
                return NotFound();
            }

            return Ok(walk);
        }
    }
}
