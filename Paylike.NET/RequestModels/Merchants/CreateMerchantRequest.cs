using Newtonsoft.Json;
using Paylike.NET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Merchants
{
    public class CreateMerchantRequest : RequestBase
    {
        public CreateMerchantRequest()
        {
            base.Uri = "/merchants";
            base.Name = "CreateMerchant";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Post;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("test")]
        public bool Test { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }

        [JsonProperty("company")]
        public Company Company { get; set; }

        [JsonProperty("bank")]
        public Bank Bank { get; set; }
    }
}
