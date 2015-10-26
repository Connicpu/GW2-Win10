using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2_Win10.API
{
    public class Item : IApiType
    {
        public string Endpoint => "/v2/items";

        public Item()
        {
            Id = 0;
            Name = "Unknown";
            Icon = "https://static.staticwars.com/quaggans/404.jpg";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public int Level { get; set; }
        [JsonProperty("vendor_value")]
        public int VendorValue { get; set; }
        [JsonProperty("default_skin")]
        public int? DefaultSkin { get; set; }
        public List<string> Flags { get; set; }
        [JsonProperty("game_types")]
        public List<string> GameTypes { get; set; }
        public List<string> Restrictions { get; set; }
        public JObject Details { get; set; }
    }
}
