namespace NZwalks.API.Model.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkingDifficultyId { get; set; }


        //navigation props

        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }

    }
}
