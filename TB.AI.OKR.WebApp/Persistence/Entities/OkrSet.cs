namespace TB.AI.OKR.WebApp.Persistence.Entities
{
    public class OkrSet : BaseEntity
    {
        public IEnumerable<OkrSetElement> OkrSetElements { get; set; }
            = new List<OkrSetElement>();

        public string Language { get; set; } = "en";

        public string? Level { get; set; }

        public string? Vision { get; set; }

        public IEnumerable<ReferenceSource> References { get; set; }
            = new List<ReferenceSource>();

        public IEnumerable<Review> Reviews { get; set; }
            = new List<Review>();

    }
}
