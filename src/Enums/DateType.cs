using System.Text.Json.Serialization;

namespace JuiceboxServer.Enums
{
    public enum DateType
    {
        [JsonPropertyName("year")]
        YEAR,

        [JsonPropertyName("month")]
        MONTH,

        [JsonPropertyName("day")]
        DAY
    }
}