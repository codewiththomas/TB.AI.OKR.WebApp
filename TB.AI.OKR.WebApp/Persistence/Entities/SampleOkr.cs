namespace TB.AI.OKR.WebApp.Persistence.Entities
{
    public class SampleOkr : Objective
    {
        public string Source { get; set; } = string.Empty;

        public IEnumerable<Review> Reviews { get; set; } 
            = new List<Review>();
    }
}
