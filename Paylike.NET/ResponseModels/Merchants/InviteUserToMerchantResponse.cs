using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.ResponseModels.Merchants
{
    public class InviteUserToMerchantResponse
    {
        [JsonProperty("isMember")]
        public bool IsMember { get; set; }
    }
}
