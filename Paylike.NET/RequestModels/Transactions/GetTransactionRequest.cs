using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Transactions
{
    public class GetTransactionRequest: RequestBase
    {
        public GetTransactionRequest()
        {
            base.UriTemplate = "/transactions/{0}";
            base.Name = "GetTransaction";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Get;
        }

        [JsonIgnore]
        private string transactionId;

        [JsonIgnore]
        public string TransactionId
        {
            get
            {
                return transactionId;
            }
            set
            {
                transactionId = value;
                base.Uri = string.Format(base.UriTemplate, transactionId);
            }
        }

    }
}
