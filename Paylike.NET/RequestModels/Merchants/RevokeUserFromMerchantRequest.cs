using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Merchants
{
    public class RevokeUserFromMerchantRequest: RequestBase
    {
        public RevokeUserFromMerchantRequest()
        {
            base.UriTemplate = "/merchants/{0}/users/{1}";
            base.HttpMethod = "DELETE";
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
                base.Uri = string.Format(base.UriTemplate, merchantId, userId);
            }
        }

        private string userId;

        [JsonIgnore]
        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                base.Uri = string.Format(base.UriTemplate, merchantId, userId);
            }
        }
    }
}
