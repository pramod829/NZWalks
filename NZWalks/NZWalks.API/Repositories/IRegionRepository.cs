﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetAsync(Guid Id);

        Task<Region> AddRegionAsync(Region region);

        Task<Region> DeleteRegionAsync(Guid id);

        Task<Region> UpdateRegionAsync(Guid id, Region region);
    }
}
