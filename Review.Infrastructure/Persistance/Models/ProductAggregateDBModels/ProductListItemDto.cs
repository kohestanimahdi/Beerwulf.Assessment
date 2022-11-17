namespace Review.Infrastructure.Persistance.Models.ProductAggregateDBModels
{
    public class ProductListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public double AverageScore { get; set; }
        public int RecommendationPercentage { get; set; }
    }
}
