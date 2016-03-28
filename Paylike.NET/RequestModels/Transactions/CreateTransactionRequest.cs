using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Transactions
{
    public class CreateTransactionRequest: RequestBase
    {
        public CreateTransactionRequest()
        {
            base.UriTemplate = "/merchants/{0}/transactions";
            base.Name = "CreateTransaction";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Post;
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

        [JsonProperty("cardId", NullValueHandling = NullValueHandling.Ignore)]
        public string CardId { get; set; }

        [JsonProperty("transactionId", NullValueHandling = NullValueHandling.Ignore)]
        public string TransactionId { get; set; }
        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("custom")]
        public Dictionary<string,string> Custom { get; set;}
    }
}
