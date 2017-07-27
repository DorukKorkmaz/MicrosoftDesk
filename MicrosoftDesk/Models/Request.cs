using System;
using Newtonsoft.Json;

namespace MicrosoftHelpDesk.Models
{
    public class Request
    {
        [JsonProperty(PropertyName = "id")]
        public String Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public String Priority { get; set; }

        [JsonProperty(PropertyName = "location")]
        public String Location { get; set; }

        [JsonProperty(PropertyName = "sublocation")]
        public String Sublocation { get; set; }

        [JsonProperty(PropertyName = "item")]
        public String Item { get; set; }

        [JsonProperty(PropertyName = "photo")]
        public String Photo { get; set; }

        [JsonProperty(PropertyName = "accessiblePhone")]
        public String AccessiblePhone { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public String Subject { get; set; }

        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }

        public Request()
        {

        }
    }
}
