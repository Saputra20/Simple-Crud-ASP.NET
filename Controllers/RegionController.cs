using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_Api.Helpers;
using Simple_Api.Models.Domain;
using Simple_Api.Models.DTO.Region;

namespace Simple_Api.Controllers
{
    [Route("api/regions")]
    public class RegionController : Controller
    {
        private readonly Database database;

        public RegionController(Database dbContext)
        {
            this.database = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            var regions = await database.Regions.ToListAsync();

            return Ok(regions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindOne(Guid id)
        {
            var region = await database.Regions.FindAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] RegionRequest Body)
        {
            var regionBody = new Region
            {
                Code = Body.Code,
                Name = Body.Name,
                RegionImageUrl = Body.RegionImageUrl
            };

            await database.Regions.AddAsync(regionBody);
            await database.SaveChangesAsync();

            var region = new RegionResponse
            {
                Id = regionBody.Id,
                Name = regionBody.Name,
                Code = regionBody.Code,
                RegionImageUrl = regionBody.RegionImageUrl
            };


            return CreatedAtAction(nameof(FindOne), new { Id = region.Id }, region);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, [FromBody] RegionRequest Body)
        {
            var regionModel = await database.Regions.FindAsync(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            regionModel.Name = Body.Name;
            regionModel.Code = Body.Code;
            regionModel.RegionImageUrl = Body.RegionImageUrl;

            await database.SaveChangesAsync();

            var region = new RegionResponse
            {
                Id = regionModel.Id,
                Name = regionModel.Name,
                Code = regionModel.Code,
                RegionImageUrl = regionModel.RegionImageUrl
            };

            return Ok(region);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var regionModel = await database.Regions.FindAsync(id);

            if(regionModel == null){
                return NotFound();
            }

            database.Regions.Remove(regionModel);
            await database.SaveChangesAsync();

            var region = new RegionResponse
            {
                Id = regionModel.Id,
                Name = regionModel.Name,
                Code = regionModel.Code,
                RegionImageUrl = regionModel.RegionImageUrl
            };

            return Ok(region);
        }

    }
}