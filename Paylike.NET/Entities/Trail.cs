using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Entities
{
    public class Trail
    {
        [JsonProperty("fee")]
        public Fee Fee { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("capture")]
        public bool Capture { get; set; }

        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }
    }
}
