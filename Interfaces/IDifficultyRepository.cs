using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple_Api.Models.Domain;
using Simple_Api.Models.DTO.Difficulty;

namespace Simple_Api.Interfaces
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();
        Task<Difficulty> CreateAsync(DifficultyRequest body);
        Task<Difficulty?> GetOneAsync(Guid id);
        Task<Difficulty> UpdateASync(Difficulty difficulty, DifficultyRequest body);
        Task<Difficulty> DeleteASync(Difficulty difficulty);
    }
}