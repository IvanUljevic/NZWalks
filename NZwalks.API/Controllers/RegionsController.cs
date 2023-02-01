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
        public async Task<IActionResult> GetAllRegions()
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
    }
}
