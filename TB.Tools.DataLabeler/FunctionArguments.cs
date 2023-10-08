using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TB.Tools.DataLabeler;

internal class FunctionArguments
{
    [JsonPropertyName("rule_applies")]
    public string RuleApplies { get; set; } = string.Empty;

    [JsonPropertyName("explanation")]
    public string? Explanation { get; set; }
}
