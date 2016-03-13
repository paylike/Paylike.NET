using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.RequestModels
{
    public class PagedRequestBase : RequestBase
    {
        public int Limit { get; set; }
        public string Before { get; set; }
        public string After { get; set; }

        public string GetPaginationQueryString()
        {
            string query = string.Format("?limit={0}", this.Limit);
            if (!string.IsNullOrEmpty(Before))
                query += "&before=" + Before;
            if (!string.IsNullOrEmpty(After))
                query += "&after=" + After;
            return query;
        }
    }
}
