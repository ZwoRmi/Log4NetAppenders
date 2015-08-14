using System.Collections.Generic;
using Newtonsoft.Json;

namespace StudioOnlineBugAppender
{
    public class Bug : IBug
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        private List<JsonPropertyVso> JsonProperties { get; set; }

        private void InitJsonProperties()
        {
            JsonProperties = new List<JsonPropertyVso>();
            if (!string.IsNullOrWhiteSpace(Title))
                this.JsonProperties.Add(new JsonPropertyVso("/fields/System.Title", this.Title));
            if (!string.IsNullOrWhiteSpace(Description))
                this.JsonProperties.Add(new JsonPropertyVso("/fields/System.Description", this.Description));
            if (!string.IsNullOrWhiteSpace(Creator))
                this.JsonProperties.Add(new JsonPropertyVso("/fields/System.CreatedBy", this.Creator));
        }

        public string Deserialize()
        {
            this.InitJsonProperties();
            return JsonConvert.SerializeObject(this.JsonProperties);
        }

        public class JsonPropertyVso
        {
            public JsonPropertyVso(string path, string value)
            {
                this.Op = "add";
                this.Value = value;
                this.Path = path;
            }
            [JsonProperty(PropertyName = "op")]
            public string Op { get; set; }
            [JsonProperty(PropertyName = "path")]
            public string Path { get; set; }
            [JsonProperty(PropertyName = "value")]
            public string Value { get; set; }
        }
    }
}