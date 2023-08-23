using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_Api.Helpers;
using Simple_Api.Interfaces;
using Simple_Api.Models.Domain;
using Simple_Api.Models.DTO.Region;

namespace Simple_Api.Controllers
{
    [Route("api/regions")]
    public class RegionController : Controller
    {
        private readonly Database database;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(Database dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.database = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            var regions = await regionRepository.GetAllAsync();

            return Ok(mapper.Map<List<RegionResponse>>(regions));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindOne(Guid id)
        {
            var region = await regionRepository.GetOneAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionResponse>(region));
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] RegionRequest Body)
        {
            var regionBody = await regionRepository.CreateAsync(Body);

            var region = mapper.Map<RegionResponse>(regionBody);

            return CreatedAtAction(nameof(FindOne), new { Id = region.Id }, region);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, [FromBody] RegionRequest Body)
        {
            var regionModel = await regionRepository.GetOneAsync(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            await regionRepository.UpdateASync(regionModel, Body);

            var region = mapper.Map<RegionResponse>(regionModel);

            return Ok(region);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var regionModel = await regionRepository.GetOneAsync(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            await regionRepository.DeleteASync(regionModel);

            var region = mapper.Map<RegionResponse>(regionModel);

            return Ok(region);
        }

    }
}