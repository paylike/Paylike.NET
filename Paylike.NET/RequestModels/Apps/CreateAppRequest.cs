using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Apps
{
    public class CreateAppRequest: RequestBase
    {
        public CreateAppRequest()
        {
            base.Uri = "/apps";
            base.Name = "CreateApp";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Post;
        }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
