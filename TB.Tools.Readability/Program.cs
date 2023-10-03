// See https://aka.ms/new-console-template for more information
using TB.Tools.Readability;

Console.WriteLine("Hello, World!");

var text = 
    "This is a test of the readability analyzer. It should " +
    "calculate the readability of this text and return a number.";

var readability = text.CalculateReadability(ReadabilityAlgorithms.AutomatedReadabilityIndex);

Console.WriteLine($"The readability of the text is {readability}.");