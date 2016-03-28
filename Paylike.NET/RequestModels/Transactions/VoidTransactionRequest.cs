using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Transactions
{
    public class VoidTransactionRequest : RequestBase
    {
        public VoidTransactionRequest()
        {
            base.UriTemplate = "/transactions/{0}/voids";
            base.Name = "VoidTransaction";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Post;
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

        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
