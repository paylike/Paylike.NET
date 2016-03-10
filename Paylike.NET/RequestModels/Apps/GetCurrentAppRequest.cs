using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels.Apps
{
    internal class GetCurrentAppRequest: RequestBase
    {
        public GetCurrentAppRequest()
        {
            this.Uri = "/me";
            this.HttpMethod = System.Net.WebRequestMethods.Http.Get;
        }
    }
}
