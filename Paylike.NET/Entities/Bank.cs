using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Entities
{
    public class Bank
    {
        [JsonProperty("iban")]
        public string IBAN { get; set; }
    }
}
