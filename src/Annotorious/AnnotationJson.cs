using System;
using System.Text.Json.Serialization;

namespace Annotorious_RazorPages_EFCore_Sample.Annotorious
{
    /// <summary>
    /// Annotorius annotation JSON
    /// </summary>
    public record AnnotationJson
    {
        [JsonPropertyName("@context")]
        public string context { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public Body[] body { get; set; }
        public Target target { get; set; }

        public record Body
        {
            public string type { get; set; }
            public string value { get; set; }
            public string purpose { get; set; }
            public DateTime created { get; set; }
            public Creator creator { get; set; }
        }
        public record Creator
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        public record Target
        {
            public Selector selector { get; set; }
        }
        public record Selector
        {
            public string type { get; set; }
            public string conformsTo { get; set; }
            public string value { get; set; }
        }
    }
}
