using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
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
        [ActionName("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var walkDifficultyDomain = await walkDifficultyRepository.GetAllAsync();
            var walkDifficulties = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficultyDomain);
            return Ok(walkDifficulties);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.GetAsync(id);
            var walkDifficultyDto = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);
            return Ok(walkDifficultyDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkingDifficulty(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code
            };
            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);
            var walkDifficultyDto = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficultyDomain.Id,
                Code = walkDifficultyDomain.Code
            };
            return CreatedAtAction(nameof(GetAllAsync), new { id = walkDifficultyDto.Id }, walkDifficultyDto);
        }

        
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficulty([FromRoute] Guid id,[FromBody] Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code
            };
            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);
            if(walkDifficultyDomain == null)
            {
                return NotFound();
            }
            var walkDifficultyDto = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficultyDomain.Id,
                Code = walkDifficultyDomain.Code
            };
            return Ok(walkDifficultyDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);
            if(walkDifficultyDomain == null)
            {
                return NotFound();
            }
            var walkDifficultyDto = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficultyDomain.Id,
                Code = walkDifficultyDomain.Code
            };
            return Ok(walkDifficultyDto);
        }
    }
}
