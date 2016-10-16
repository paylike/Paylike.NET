using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Merchants
{
    public class UpdateMerchantRequest : RequestBase
    {
        public UpdateMerchantRequest()
        {
            base.UriTemplate = "/merchants/{0}";
            base.Name = "UpdateMetchant";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Put;
        }

        private string merchantId;

        [JsonIgnore]
        public string MerchantId
        {
            get
            {
                return merchantId;
            }
            set
            {
                merchantId = value;
                base.Uri = string.Format(base.UriTemplate, merchantId);
            }
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }

    }
}
