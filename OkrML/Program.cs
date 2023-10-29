// See https://aka.ms/new-console-template for more information
using OkrML;
using OkrML.WebApi.Models;

#region check deterministic bahavior

var requests = new List<ClassificationRequest>
{
    new ClassificationRequest { LabelName = @"rule_11", Type = "objective", Text = @"Grow globally by increasing international revenue." }
};

int repetitions = 100;

foreach (var request in requests)
{
    for (int i = 0; i < repetitions; i++)
    {
        var data = new OkrSetElementLabeler.ModelInput()
        {
            Text = request.Text,
            Type = request.Type,
            LabelName = request.LabelName,
        };
        var result = OkrSetElementLabeler.Predict(data);
        Console.WriteLine($"{i.ToString("000")}\tValue = {result.PredictedLabel}\tp = {result.Probability}\tLabel = {request.LabelName}\t{request.Type}\t{request.Text.Substring(0, 10)}...");
    }
}

//Load sample data




#endregion

//Load model and predict output

Console.WriteLine();