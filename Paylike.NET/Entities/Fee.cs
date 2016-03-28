using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Entities
{
    public class Fee
    {
        [JsonProperty("flat")]
        public int Flat { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }
    }
}
