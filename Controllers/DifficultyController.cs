using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Simple_Api.Helpers;
using Simple_Api.Interfaces;
using Simple_Api.Models.DTO.Difficulty;

namespace Simple_Api.Controllers
{
    [Route("api/difficulties")]
    public class DifficultyController : Controller
    {
       private readonly Database database;
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

        public DifficultyController(Database dbContext, IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            this.database = dbContext;
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
        } 


        [HttpGet]
        [ProducesResponseType(typeof(DifficultyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            var difficulties = await difficultyRepository.GetAllAsync();

            return Ok(mapper.Map<List<DifficultyResponse>>(difficulties));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DifficultyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindOne(Guid id)
        {
            var difficulty = await difficultyRepository.GetOneAsync(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DifficultyResponse>(difficulty));
        }

        [HttpPost]
        [ProducesResponseType(typeof(DifficultyResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] DifficultyRequest Body)
        {
            var difficultyModel = await difficultyRepository.CreateAsync(Body);

            var difficulty = mapper.Map<DifficultyResponse>(difficultyModel);

            return CreatedAtAction(nameof(FindOne), new { Id = difficulty.Id }, difficulty);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DifficultyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, [FromBody] DifficultyRequest Body)
        {
            var difficultyModel = await difficultyRepository.GetOneAsync(id);

            if (difficultyModel == null)
            {
                return NotFound();
            }

            await difficultyRepository.UpdateASync(difficultyModel, Body);

            var difficulty = mapper.Map<DifficultyResponse>(difficultyModel);

            return Ok(difficulty);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DifficultyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var difficultyModel = await difficultyRepository.GetOneAsync(id);

            if (difficultyModel == null)
            {
                return NotFound();
            }

            await difficultyRepository.DeleteASync(difficultyModel);

            var difficulty = mapper.Map<DifficultyResponse>(difficultyModel);

            return Ok(difficulty);
        }
    }
}