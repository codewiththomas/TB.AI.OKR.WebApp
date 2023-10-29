using System.ComponentModel;

namespace TB.Tools.DataLabeler;

public enum LabelProviders
{
    [Description("annotator")]
    Annotator = 0,

    [Description("gpt-3.5-turbo")]
    OpenAI_GPT_35_Turbo = 135,
    OpenAI_GPT_4 = 140,

    [Description("gpt4all-falcon-q4_0.gguf")]
    GPT4All_Falcon = 200,

    [Description("nous-hermes-llama2-13b.Q4_0.gguf")]
    GPT4All_Hermes_LLaMA2 = 201,

    [Description("ml.net")]
    ML = 300
}
