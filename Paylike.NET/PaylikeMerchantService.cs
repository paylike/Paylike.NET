using Paylike.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylike.NET.Entities;
using Paylike.NET.RequestModels.Merchants;
using Paylike.NET.ResponseModels;
using Newtonsoft.Json.Linq;

namespace Paylike.NET
{
    public class PaylikeMerchantService : BaseService, IPaylikeMerchantService
    {
        public PaylikeMerchantService(string privateApiKey): base(privateApiKey)
        {

        }

        public ApiResponse<Merchant> CreateMerchant(CreateMerchantRequest request)
        {
            return SendApiRequest<CreateMerchantRequest, Merchant>(request);
        }

        public ApiResponse<Merchant> GetMerchant(GetMerchantRequest request)
        {
            return SendApiRequest<GetMerchantRequest, Merchant>(request);
        }

        public ApiResponse<object> UpdateMerchant(UpdateMerchantRequest request)
        {
            return SendApiRequest<UpdateMerchantRequest, object>(request);
        }

        protected override string ProcessApiResponse(string json, string requestName)
        {
            string processedJson = json;
            switch(requestName)
            {
                case "CreateMetchant":
                    {
                        processedJson = JObject.Parse(json).SelectToken("merchant").ToString();
                        break;
                    }
                case "GetMetchant":
                    {
                        processedJson = JObject.Parse(json).SelectToken("merchant").ToString();
                        break;
                    }
            }

            return processedJson;
        }

    }
}
