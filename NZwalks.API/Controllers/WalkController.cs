using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Repositories;
using NZwalks.API.Model.Domain;

namespace NZwalks.API.Controllers

{

    [ApiController]
    [Route("Walks")]


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
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //fetch data from database - domain walks
            var walkDomain = await walkRepository.GetAllAsync();

            // convert domain walks to DTO walks
            var walkDTO = mapper.Map<List<Model.DTO.Walk>>(walkDomain);

            // return walks


            return Ok(walkDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //get walk domain from database
            var walkDomain = await walkRepository.GetAsync(id);

            //convert domain to DTO
            var walkDTO = mapper.Map<Model.DTO.Walk>(walkDomain);

            //return Ok
            return Ok(walkDTO);
        }

        [HttpPost]

        public async Task<IActionResult> AddWalkAsync([FromBody] Model.DTO.AddWalkRequest addWalkRequest)
        {
            // Convert DTO to domain 
            var walkDomain = new Model.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };

            //pass domain to repository
            walkDomain = await walkRepository.AddAsync(walkDomain);

            //convert domain to DTO 
            var walkDTO = new Model.DTO.Walk
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                WalkDifficultyId = walkDomain.WalkDifficultyId,
            };
            //send DTO response back 
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,
            [FromBody] Model.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //convert DTO to domain object
            var walkDomain = new Model.Domain.Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };

            // pass details to repository - get domain object in response (or null)
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);
            //handle null (not found) 
            if (walkDomain == null)
            {
                return NotFound("Walk with this ID was not found");
            }
            else
            {
                //convert back domain to DTO 
                var walkDTO = new Model.DTO.Walk
                {
                    Id = walkDomain.Id,
                    Length = walkDomain.Length,
                    Name = walkDomain.Name,
                    RegionId = walkDomain.RegionId,
                    WalkDifficultyId = walkDomain.WalkDifficultyId,
                };
                //return response
                return Ok(walkDTO);

            }

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // call repository to delete walk
            var walkDomain = await walkRepository.DeleteAsync(id);

            if(walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Model.DTO.Walk>(walkDomain);

            return Ok(walkDTO);
        }
    }
}
