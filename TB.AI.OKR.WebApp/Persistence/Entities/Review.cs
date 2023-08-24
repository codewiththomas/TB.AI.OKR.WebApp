namespace TB.AI.OKR.WebApp.Persistence.Entities
{
    public class Review : BaseEntity
    {
        public DateTime Created { get; set; }

        public TimeSpan ReviewTime { get; set; }

        public string Provider { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string? Result { get; set; }
    }
}
