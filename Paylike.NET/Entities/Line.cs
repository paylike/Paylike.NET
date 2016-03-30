using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Entities
{
    public class Line
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        [JsonProperty("merchantId")]
        public string MerchantId { get; set; }
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }
        [JsonProperty("amount")]
        public AmountDto Amount { get; set; }
        [JsonProperty("balance")]
        public int Balance { get; set; }
        [JsonProperty("capture")]
        public bool Capture { get; set; }
        [JsonProperty("fee")]
        public int Fee { get; set; }

    }

    public class AmountDto
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
