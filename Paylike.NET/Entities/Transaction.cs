using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Entities
{
    public class Transaction
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("refundedAmount")]
        public int RefundedAmount { get; set; }

        [JsonProperty("capturedAmount")]
        public int CapturedAmount { get; set; }

        [JsonProperty("voidedAmount")]
        public int VoidedAmount { get; set; }

        [JsonProperty("pendingAmount")]
        public int PendingAmount { get; set; }

        [JsonProperty("disputedAmount")]
        public int DisputedAmount { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("custom")]
        public Dictionary<string,string> Custom { get; set; }

        [JsonProperty("successful")]
        public bool Successful { get; set; }

        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }

        [JsonProperty("trail")]
        public Trail[] Trail { get; set; }
    }
}
