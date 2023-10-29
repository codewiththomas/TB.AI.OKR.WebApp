namespace OkrML.WebApi.Models
{
    public class ClassificationRequest
    {
        public string Text { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string LabelName { get; set; } = string.Empty;
    }
}
