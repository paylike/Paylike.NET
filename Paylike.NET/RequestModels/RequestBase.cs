using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels
{
    public abstract class RequestBase
    {
        internal string Uri { get; set; }
        internal string HttpMethod { get; set; }
        protected internal string UriTemplate { get; set; }
        [JsonIgnore]
        protected internal string Name { get; set; }
    }
}
