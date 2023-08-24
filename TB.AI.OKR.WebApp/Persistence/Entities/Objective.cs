namespace TB.AI.OKR.WebApp.Persistence.Entities
{
    public class Objective : BaseEntity
    {
        public string Text { get; set; } = string.Empty;

        public IEnumerable<KeyResult> KeyResults { get; set; }
            = new List<KeyResult>();
    }
}
