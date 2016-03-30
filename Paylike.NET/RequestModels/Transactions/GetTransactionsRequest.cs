using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Transactions
{
    public class GetTransactionsRequest : PagedRequestBase
    {
        public GetTransactionsRequest()
        {
            base.UriTemplate = "/merchants/{0}/transactions";
            base.Name = "GetTransactions";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Get;
        }

        [JsonIgnore]
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
