using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Model.Domain; 
using NZwalks.API.Repositories;
using System.Collections.Generic;

namespace NZwalks.API.Controllers
{
    [ApiController]
    [Route("Regions")]

    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionsRepository;
        private readonly IMapper mapper;


        public RegionsController(IRegionRepository regionsRepository, IMapper mapper)
        {
            this.regionsRepository = regionsRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionsRepository.GetAllAsync();

            // return DTO regions

            //var regionsDTO = new List<API.Model.DTO.Region>();

            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new API.Model.DTO.Region()
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

            var regionsDTO = mapper.Map <List<Model.DTO.Region >> (regions);

           return Ok(regions);
       }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionsRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Model.DTO.Region>(region);

            return Ok(region);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Model.DTO.AddRegionRequest addRegionRequest)
        {
            //Request(DTO) to Domain model
            var region = new Model.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population,

            };

            //Pass details to repository
            region = await regionsRepository.AddAsync(region);


            //Convert data back to DTO 

            var regionDTO = new Model.DTO.Region
            {
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = (long)region.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get region from database
            var region = await regionsRepository.DeleteAsync(id);
            
            
            //if null return"Notfound"
            if (region == null)
            {
                return NotFound();
            }
            //convert response back to DTO 
            var regionDTO = new Model.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = (long)region.Population,
            };

            //return Ok response
            return Ok(regionDTO);


        }
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Model.DTO.UpdateRegionRequest updateRegionRequest )
        {
            //convert DTO to domain model
            var region = new Model.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = (long)updateRegionRequest.Population,
            };
            //update Region using repository 
            region = await regionsRepository.UpdateAsync(id, region);

            //if null then NOtFound
            if (region == null)
            {
                return NotFound();
            }
            //convert domain back to DTO 
            var regionDTO = new Model.DTO.Region()
            {
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = (long)region.Population,
            };

            //return Ok response
            return Ok(regionDTO);
        }
    }
}
