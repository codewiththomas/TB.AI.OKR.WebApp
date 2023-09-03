using System.ComponentModel.DataAnnotations;

namespace TB.AI.OKR.WebApp.Persistence.Entities
{
    public class Okr : BaseEntity
    {
        public string Objective { get; set; } = string.Empty;        

        [Range(0, 1)]
        public double IsSpecificRating { get; set; }

        [Range(0, 1)]
        public double IsMeasurableRating { get; set; }

        [Range(0, 1)]
        public double IsArchievableRating { get; set; }

        [Range(0, 1)]
        public double IsRelevantRating { get; set; }

        [Range(0,1)]
        public double IsTimeBoundedRating { get; set; }

        public string Language { get; set; } = "en";

        public IEnumerable<KeyResult> KeyResults { get; set; }
            = new List<KeyResult>();

        public IEnumerable<ReferenceSource> References { get; set; }
            = new List<ReferenceSource>();

        public IEnumerable<Review> Reviews { get; set; }
            = new List<Review>();

    }
}
