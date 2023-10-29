using OkrML.WebApi.Models;

namespace OkrML;

public class PredictionService
{
    public bool PredictLabel(ClassificationRequest request)
    {
        //Load sample data
        var data = new OkrSetElementLabeler.ModelInput()
        {
            Text = request.Text,
            Type = request.Type,
            LabelName = request.LabelName,
        };

        //Load model and predict output
        var result = OkrSetElementLabeler.Predict(data);

        return result.PredictedLabel;
    }
}
