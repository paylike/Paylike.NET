using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Merchants
{
    public class InviteUserToMerchantRequest: RequestBase
    {
        public InviteUserToMerchantRequest()
        {
            base.UriTemplate = "/merchants/{0}/users";
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

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
