using Newtonsoft.Json;
using Paylike.NET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.ResponseModels.Apps
{
    public class CreateAppResponse
    {
        [JsonProperty("app")]
        public App App { get; set; }
    }
}
