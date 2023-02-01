using NZwalks.API.Model.Domain;

namespace NZwalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

    }
}
