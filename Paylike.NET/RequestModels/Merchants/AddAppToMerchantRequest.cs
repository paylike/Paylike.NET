using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Merchants
{
    public class AddAppToMerchantRequest : RequestBase
    {
        public AddAppToMerchantRequest()
        {
            base.UriTemplate = "/merchants/{0}/apps";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Post;
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

        [JsonProperty("appId")]
        public string AppId { get; set; }
    }
}
