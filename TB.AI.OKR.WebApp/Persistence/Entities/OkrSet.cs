namespace TB.AI.OKR.WebApp.Persistence.Entities
{
    public class OkrSet : BaseEntity
    {
        public IList<OkrSetElement> OkrSetElements { get; set; }
            = new List<OkrSetElement>();

        public string? Comment { get; set; }

        public string Language { get; set; } = "en";

        public string? Level { get; set; }

        public string? Vision { get; set; }

        public string? AuthorsRating { get; set; }

        public bool UseForSampleDataset { get; set; } = true;

        public IList<ReferenceSource> References { get; set; }
            = new List<ReferenceSource>();

        public IEnumerable<Review> Reviews { get; set; }
            = new List<Review>();

    }
}
