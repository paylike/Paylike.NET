using Paylike.NET.Entities;
using Paylike.NET.RequestModels.Apps;
using Paylike.NET.ResponseModels;
using Paylike.NET.ResponseModels.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Interfaces
{
    public interface IPaylikeAppService
    {
        ApiResponse<App> CreateApp(CreateAppRequest request);
        ApiResponse<GetCurrentAppResponse> GetCurrentApp();
    
        void SetApiKey(string privateApiKey);
    }
}
