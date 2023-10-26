// See https://aka.ms/new-console-template for more information
using OkrML;

Console.WriteLine("Hello, World!");

//Load sample data
var sampleData = new OkrSetElementLabeler.ModelInput()
{
    Text = @"Grow globally by increasing international revenue.",
    Type = @"objective",
    LabelName = @"rule_8",
};

//Load model and predict output
var result = OkrSetElementLabeler.Predict(sampleData);

Console.WriteLine();