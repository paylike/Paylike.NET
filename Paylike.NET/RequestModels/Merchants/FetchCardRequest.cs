using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Merchants
{
    public class FetchCardRequest : RequestBase
    {
        public FetchCardRequest()
        {
            base.UriTemplate = "/cards/{0}";
            base.Name = "FetchCard";
            base.HttpMethod = System.Net.WebRequestMethods.Http.Get;
        }

        [JsonIgnore]
        private string cardId;

        [JsonIgnore]
        public string CardID
        {
            get
            {
                return cardId;
            }
            set
            {
                cardId = value;
                base.Uri = string.Format(base.UriTemplate, cardId);
            }
        }
    }
}
