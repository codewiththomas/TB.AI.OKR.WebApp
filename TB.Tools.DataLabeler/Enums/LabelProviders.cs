using System.ComponentModel;

namespace TB.Tools.DataLabeler;

public enum LabelProviders
{
    [Description("Annotator")]
    Base = 0,

    [Description("gpt-3.5-turbo")]
    OpenAI_gpt35turbo = 135,
    OpenAI_gpt4 = 140,

    [Description("gpt4all-falcon-q4_0.gguf")]
    GPT4All_falcon = 200,

    [Description("nous-hermes-llama2-13b.Q4_0.gguf")]
    GPT4All_hermes_llama2 = 201,

    [Description("Machine Learning Model from ML.NET")]
    ML = 300
}
