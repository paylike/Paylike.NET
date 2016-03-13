using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Merchants
{
    public class GetMerchantAppsRequest : PagedRequestBase
    {
        public GetMerchantAppsRequest()
        {
            base.UriTemplate = "/merchants/{0}/apps";
            base.Name = "GetMerchantApps";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Get;
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

    }
}
