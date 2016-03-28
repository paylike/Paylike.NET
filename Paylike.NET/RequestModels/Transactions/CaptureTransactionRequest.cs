using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Transactions
{
    public class CaptureTransactionRequest : RequestBase
    {
        public CaptureTransactionRequest()
        {
            base.UriTemplate = "/transactions/{0}/captures";
            base.Name = "CaptureTransaction";
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

        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
