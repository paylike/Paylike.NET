using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.ResponseModels
{
    public class ApiResponse<T> where T : class, new()
    {
        public ApiResponse()
        {
            this.Content = new T();
        }

        public bool IsError
        {
            get { return !string.IsNullOrEmpty(ErrorContent) || !string.IsNullOrEmpty(ErrorMessage); }
        }

        public int ResponseCode { get; set; }

        public string ErrorContent { get; internal set; }

        public string ErrorMessage { get; internal set; }

        public T Content { get; internal set; }
    }
}
