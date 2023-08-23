using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simple_Api.Helpers;
using Simple_Api.Interfaces;
using Simple_Api.Models.Domain;
using Simple_Api.Models.DTO.Region;

namespace Simple_Api.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly Database database;
        public RegionRepository(Database database)
        {
            this.database = database;
        }

        public async Task<Region> CreateAsync(RegionRequest body)
        {
            var region = new Region
            {
                Code = body.Code,
                Name = body.Name,
                RegionImageUrl = body.RegionImageUrl
            };

            await database.Regions.AddAsync(region);
            await database.SaveChangesAsync();

            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await database.Regions.ToListAsync();
        }

        public async Task<Region?> GetOneAsync(Guid id)
        {
            return await database.Regions.FindAsync(id);
        }

        public async Task<Region> UpdateASync(Region region, RegionRequest body)
        {
            region.Name = body.Name;
            region.Code = body.Code;
            region.RegionImageUrl = body.RegionImageUrl;

            await database.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteASync(Region region)
        {
            database.Regions.Remove(region);
            await database.SaveChangesAsync();
            return region;
        }
    }
}