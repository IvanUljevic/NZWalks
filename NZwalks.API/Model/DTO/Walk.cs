

using NZwalks.API.Model.Domain;

namespace NZwalks.API.Model.DTO
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }


        //navigation props

        public Region Region { get; set; }
        public Domain.WalkDifficulty WalkDifficulty { get; set; }

    }
}
