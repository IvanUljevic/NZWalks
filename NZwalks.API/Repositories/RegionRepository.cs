using Microsoft.EntityFrameworkCore;
using NZwalks.API.Controllers.Data;
using NZwalks.API.Model.Domain;

namespace NZwalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZwalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {

        }      
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZwalksDbContext.Regions.ToListAsync();
        }
    }
}
