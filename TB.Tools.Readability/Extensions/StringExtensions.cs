using System.Text.RegularExpressions;

namespace TB.Tools.Readability;

public static class StringExtensions
{
    public static double CalculateReadability(this string text, ReadabilityAlgorithms algorithm)
    {
        return algorithm switch
        {
            ReadabilityAlgorithms.AutomatedReadabilityIndex => CalculateReadabilityARI(text),
            _ => throw new NotImplementedException()
        };
    }


    /// <summary>
    /// Counts the number of sentences in a text.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static int CountSentences(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }

        var sentences = text.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        return sentences.Length;
    }
    

    /// <summary>
    /// Counts the number of words in a text.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static int CountWords(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }            

        var words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }


    /// <summary>
    /// Counts the number of characters in a text.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static int CountCharacters(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }

        var characters = text.Count(c => !char.IsWhiteSpace(c));

        return characters;
    }


    /// <summary>
    /// Counts the number of syllables in a word (approximation).
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public static int CountSyllables(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return 0;
        }

        word = word.ToLower(); // Convert the word to lowercase

        // Regex to count the number of vowel groups. This will usually correspond to the number of syllables.
        var matches = Regex.Matches(word, "[aeiouy]+");

        int count = matches.Count;

        // Subtract one syllable for words ending in "e" except for words with a single syllable
        if (word.EndsWith("e") && count > 1)
        {
            count--;
        }

        return count;
    }

    #region Helpers

    /// <summary>
    /// Returns the readability of a text according to the Automated Readability Index.
    /// </summary>
    /// <param name="text"></param>
    /// <returns>Grade level. A score between 1 and 14.</returns>
    private static double CalculateReadabilityARI(string text)
    {
        var avgCharsPerWord = (double)text.CountCharacters() / text.CountWords();
        var avgWordsPerSentence = (double)text.CountWords() / text.CountSentences();
        var readability = 4.71 * avgCharsPerWord + 0.5 * avgWordsPerSentence - 21.43;
        return Math.Ceiling(readability);
    }

    #endregion
}
