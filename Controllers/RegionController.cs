using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            var regions = database.Regions.ToList();

            return Ok(regions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status200OK)]
        public IActionResult FindOne(Guid id)
        {
            var region = database.Regions.Find(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RegionResponse), StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] RegionRequest Body)
        {
            var regionBody = new Region
            {
                Code = Body.Code,
                Name = Body.Name,
                RegionImageUrl = Body.RegionImageUrl
            };

            database.Regions.Add(regionBody);
            database.SaveChanges();

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
        public IActionResult Update(Guid id, [FromBody] RegionRequest Body)
        {
            var regionModel = database.Regions.Find(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            regionModel.Name = Body.Name;
            regionModel.Code = Body.Code;
            regionModel.RegionImageUrl = Body.RegionImageUrl;

            database.SaveChanges();

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
        public IActionResult Delete(Guid id)
        {
            var regionModel = database.Regions.Find(id);

            if(regionModel == null){
                return NotFound();
            }

            database.Regions.Remove(regionModel);
            database.SaveChanges();

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