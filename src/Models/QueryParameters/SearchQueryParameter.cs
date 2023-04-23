using JuiceboxServer.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JuiceboxServer.Models.QueryParameters
{
    public class SearchQueryParameter
    {
        [FromQuery(Name ="q")]
        public string Query { get; set; } = null!;

        [FromQuery(Name = "type")]
        public string Type { get; set; } = null!;

        [FromQuery(Name = "market")]
        public string? Market { get; set; }

        [FromQuery(Name = "limit")]
        public int? Limit { get; set; }

        [FromQuery(Name = "offset")]
        public int? Offset { get; set; }

        [FromQuery(Name = "include_external")]
        public string? IncludeExternal { get; set; }
    }
}