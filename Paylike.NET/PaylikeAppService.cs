using Paylike.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylike.NET.RequestModels.Apps;
using Paylike.NET.ResponseModels;
using Paylike.NET.ResponseModels.Apps;

namespace Paylike.NET
{
    public class PaylikeAppService : BaseService, IPaylikeAppService
    {
        public PaylikeAppService(string privateKey) : 
            base(privateKey)
        {

        }

        public ApiResponse<CreateAppResponse> CreateApp(CreateAppRequest request)
        {
            return SendApiRequest<CreateAppRequest, CreateAppResponse>(request);
        }

        public ApiResponse<GetCurrentAppResponse> GetCurrentApp()
        {
            return SendApiRequest<GetCurrentAppRequest, GetCurrentAppResponse>(new GetCurrentAppRequest());
        }

        public ApiResponse<object> AddAppToMerchant(AddAppToMerchantRequest request)
        {
            return SendApiRequest<AddAppToMerchantRequest, object>(request);
        }

        public void SetApiKey(string privateApiKey)
        {
            this._apiClient.DefaultRequestHeaders.Remove("Authorization");
            this._apiClient.DefaultRequestHeaders.Add("Authorization", GetAuthorizationHeaderValue(privateApiKey));
        }
    }
}
