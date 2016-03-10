using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.ResponseModels.Apps
{
    public class GetCurrentAppResponse
    {
        [JsonProperty("identity")]
        public Identity Identity { get; set; }
    }

    public class Identity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
