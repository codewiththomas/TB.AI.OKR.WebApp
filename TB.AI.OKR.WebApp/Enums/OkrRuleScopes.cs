using System.ComponentModel;

namespace TB.AI.OKR.WebApp;

public enum OkrRuleScopes
{
    [Description("Global")]
    Global = 0,

    [Description("OKR set")]
    OkrSet = 1,

    [Description("Objective")]
    Objective = 2,

    [Description("Key Result")]
    KeyResult = 3
}
