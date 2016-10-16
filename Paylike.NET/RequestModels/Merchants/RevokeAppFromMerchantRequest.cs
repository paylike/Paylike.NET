using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Merchants
{
    public class RevokeAppFromMerchantRequest: RequestBase
    {
        public RevokeAppFromMerchantRequest()
        {
            base.UriTemplate = "/merchants/{0}/apps/{1}";
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
                base.Uri = string.Format(base.UriTemplate, merchantId, appId);
            }
        }

        private string appId;

        [JsonIgnore]
        public string AppId
        {
            get
            {
                return appId;
            }
            set
            {
                appId = value;
                base.Uri = string.Format(base.UriTemplate, merchantId, appId);
            }
        }
    }
}
