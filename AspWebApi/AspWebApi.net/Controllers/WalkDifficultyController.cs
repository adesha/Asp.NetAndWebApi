using AspWebApi.net.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AspWebApi.net.Models.DTO;

namespace AspWebApi.net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;
        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResultsAsync()
        {
            var walkd=await walkDifficultyRepository.GetAllAsync();
            var walkdDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkd);
            return Ok(walkdDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync([FromRoute]Guid id)
        {
            var walkd = await walkDifficultyRepository.GetWalkDifficultyByIdAsync(id);
            if (walkd == null)
            {
                return NotFound();
            }
            var walkdDTO = new Models.DTO.WalkDifficulty()
            {
                Code = walkd.Code,
                Id = walkd.Id,
            };
            return Ok(walkdDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody]AddWalkDifficultyRequest AddwalkDifficulty)
        {
            var walkd = new Models.Domain.WalkDifficulty
            {
                Code = AddwalkDifficulty.Code
            };
            walkd=await walkDifficultyRepository.AddWalkDifficultyAsync(walkd);

            var walkdDTO = new Models.DTO.WalkDifficulty
            {
                Id = walkd.Id,
                Code = walkd.Code
            };
            return Ok(walkdDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute]Guid id, [FromBody]UpdateWalkDifficultyRequest UpdatewalkDifficulty)
        {
            var walkd = new Models.Domain.WalkDifficulty
            {
                Code = UpdatewalkDifficulty.Code
            };
            var uwalkd = await walkDifficultyRepository.UpdateWalkDifficultyAsync(id, walkd);
            if(uwalkd == null)
            {
                return NotFound();
            }
            var walkdDTO = new Models.DTO.WalkDifficulty
            {
                Id = uwalkd.Id,
                Code = uwalkd.Code
            };
            return Ok(walkdDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
        {
            var walkd = await walkDifficultyRepository.DeleteAsync(id);
            if (walkd == null)
            {
                return NotFound();
            }
            var walkdDTO = new Models.DTO.WalkDifficulty
            {
                Id = walkd.Id,
                Code = walkd.Code
            };
            return Ok(walkdDTO);
        }
    }
}
