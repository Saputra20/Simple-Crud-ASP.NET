using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple_Api.Models.Domain;
using Simple_Api.Models.DTO.Region;

namespace Simple_Api.Interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> CreateAsync(RegionRequest body);
        Task<Region?> GetOneAsync(Guid id);
        Task<Region> UpdateASync(Region region, RegionRequest body);
        Task<Region> DeleteASync(Region region);
    }
}