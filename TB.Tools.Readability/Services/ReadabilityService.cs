using System.Xml.Linq;

namespace TB.Tools.Readability;

public class ReadabilityService : IReadabilityService
{
    public IEnumerable<string> GetAvailableAlgorithms()
    {
        foreach (ReadabilityAlgorithms algorithm in (ReadabilityAlgorithms[]) Enum.GetValues(typeof(ReadabilityAlgorithms)))
        {
            yield return algorithm.ToString();
        }
    }


    public double CalculateReadability(string text, string algorithm)
    {
        ReadabilityAlgorithms selectedAlgorithm;
        var parseSuccess = Enum.TryParse(algorithm, out selectedAlgorithm);

        if (!parseSuccess)
        {
            throw new ArgumentException($"{algorithm} is not a valid algorithm.");
        }

        return text.CalculateReadability(selectedAlgorithm);
    }
}
