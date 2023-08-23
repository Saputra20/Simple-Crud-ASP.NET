using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simple_Api.Helpers;
using Simple_Api.Interfaces;
using Simple_Api.Models.Domain;
using Simple_Api.Models.DTO.Difficulty;

namespace Simple_Api.Repositories
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly Database database;
        public DifficultyRepository(Database database)
        {
            this.database = database;
        }
        
        public async Task<Difficulty> CreateAsync(DifficultyRequest body)
        {
            var difficulty = new Difficulty
            {
                Name = body.Name,
            };

            await database.Difficulties.AddAsync(difficulty);
            await database.SaveChangesAsync();

            return difficulty;
        }

        public async Task<List<Difficulty>> GetAllAsync()
        {   
            return await database.Difficulties.ToListAsync();
        }

        public async Task<Difficulty?> GetOneAsync(Guid id)
        {
            return await database.Difficulties.FindAsync(id);
        }

        public async Task<Difficulty> UpdateASync(Difficulty difficulty, DifficultyRequest body)
        {
            difficulty.Name = body.Name;

            await database.SaveChangesAsync();

            return difficulty;
        }

        public async Task<Difficulty> DeleteASync(Difficulty difficulty)
        {
            database.Difficulties.Remove(difficulty);
            await database.SaveChangesAsync();
            return difficulty;
        }
    }
}