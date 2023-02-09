using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Controllers.Data;
using NZwalks.API.Model.Domain;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController: Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultyAsync()
        {
            var walkDifficulty = await walkDifficultyRepository.GetAllAsync();
            //Convert domain to DTO 
            var walkDifficultyDTO = mapper.Map<List<Model.DTO.WalkDifficulty>>(walkDifficulty);
          
            return Ok(walkDifficultyDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var wakDifficultyDomain = await walkDifficultyRepository.GetAsync(id);
            
            if (wakDifficultyDomain == null)
            {
                return NotFound();
            }
            var walkDifficultyDTO = mapper.Map<Model.DTO.WalkDifficulty>(wakDifficultyDomain);
            //return Ok
            return Ok(walkDifficultyDTO);
        }



        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync
           (Model.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {       //Convert DTO to Domain model
            var walkDifficultyDomain = new Model.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code,
            };
            // Call repository
            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            // Convert to DTO 
            var walkDifficultyDTO = mapper.Map<Model.DTO.WalkDifficulty>(walkDifficultyDomain);

            //return response
            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }



        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id,
            Model.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            //convert DTO to domain object
            var walkDifficultyDomain = new Model.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code,
            };

            // pass details to repository - get domain object in response (or null)
            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);
            //handle null (not found) 
            if (walkDifficultyDomain == null)
            {
                return NotFound("Walk with this ID was not found");
            }
            else
            {
                //convert back domain to DTO 
                var walkDifficultyDTO = new Model.DTO.WalkDifficulty
                {
                    Code = walkDifficultyDomain.Code,
                };
                //return response
                return Ok(walkDifficultyDTO);

            }
        }
        [HttpDelete]
        [Route("{id:guid}")]


        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            // call repository to delete walk
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }
            var walkDifficultyDTO = mapper.Map<Model.DTO.WalkDifficulty>(walkDifficultyDomain);

            return Ok(walkDifficultyDTO);
        }
       
    }
}
        

    
    






