using System.ComponentModel.DataAnnotations;

namespace TB.AI.OKR.WebApp.Persistence.Entities
{
    public class Objective : BaseEntity
    {
        public string Text { get; set; } = string.Empty;        

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

        public IEnumerable<KeyResult> KeyResults { get; set; }
            = new List<KeyResult>();
    }
}
