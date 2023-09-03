namespace TB.AI.OKR.WebApp.Persistence.Entities
{
    public class ReferenceSource
    {
        public int Id { get; set; }

        public ReferenceSourceTypes ReferenceSourceType { get; set; }

        public string? Title { get; set; }

        public string? SubTitle { get; set; }

        public string? Authors { get; set; }

        public string? Year { get; set; }

        public string? URL { get; set; }

        public string? DOI { get; set; }

        public string? Publisher { get; set; }

        public IList<OkrRule>? OkrRules { get; set; }
    }
}
