using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Merchants
{
    public class GetMerchantsRequest: PagedRequestBase
    {
        public GetMerchantsRequest()
        {
            base.UriTemplate = "/identities/{0}/merchants";
            base.Name = "GetMerchants";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Get;
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
                base.Uri = string.Format(base.UriTemplate, appId);
            }
        }

    }
}
