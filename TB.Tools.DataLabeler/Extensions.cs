namespace TB.Tools.DataLabeler;

public static class Extensions
{
    public static string GetOpenAiModelName(this LabelProviders labelProvider)
    {
        var result = labelProvider switch
        {
            LabelProviders.Annotator => "gpt-3.5-turbo",
            LabelProviders.OpenAI_GPT_35_Turbo => "gpt-3.5-turbo",
            LabelProviders.OpenAI_GPT_4 => "gpt-4",
            _ => string.Empty
        };
        return result;
    }
}
