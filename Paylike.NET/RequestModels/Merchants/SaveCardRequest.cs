using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Merchants
{
    public class SaveCardRequest : RequestBase
    {
        public SaveCardRequest()
        {
            base.UriTemplate = "/merchants/{0}/cards";
            base.Name = "SaveCard";
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

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }
    }
}
