using Paylike.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylike.NET.RequestModels.Apps;
using Paylike.NET.ResponseModels;
using Paylike.NET.ResponseModels.Apps;
using Newtonsoft.Json.Linq;
using Paylike.NET.Entities;

namespace Paylike.NET
{
    public class PaylikeAppService : BaseService, IPaylikeAppService
    {
        public PaylikeAppService(string privateKey) : 
            base(privateKey)
        {

        }

        public ApiResponse<App> CreateApp(CreateAppRequest request)
        {
            return SendApiRequest<CreateAppRequest, App>(request);
        }

        public ApiResponse<GetCurrentAppResponse> GetCurrentApp()
        {
            return SendApiRequest<GetCurrentAppRequest, GetCurrentAppResponse>(new GetCurrentAppRequest());
        }

        protected override string ProcessApiResponse(string json, string requestName)
        {
            string processedJson = json;
            switch (requestName)
            {
                case "CreateApp":
                    {
                        processedJson = JObject.Parse(json).SelectToken("app").ToString();
                        break;
                    }
            }

            return processedJson;
        }

        public void SetApiKey(string privateApiKey)
        {
            this._apiClient.DefaultRequestHeaders.Remove("Authorization");
            this._apiClient.DefaultRequestHeaders.Add("Authorization", GetAuthorizationHeaderValue(privateApiKey));
        }
    }
}
