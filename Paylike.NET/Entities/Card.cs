using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Entities
{
    public class Card
    {
        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("bin")]
        public string Bin { get; set; }

        [JsonProperty("last4")]
        public string Last4 { get; set; }

        [JsonProperty("expiry")]
        public DateTime Expiry { get; set; }

        [JsonProperty("scheme")]
        public string Scheme { get; set; }

        [JsonProperty("deleted")]
        public DateTime? Deleted { get; set; }
    }
}
