namespace TB.Tools.Readability;

public interface IReadabilityService
{
    IEnumerable<string> GetAvailableAlgorithms();

    double CalculateReadability(string text, string algorithm);

}
