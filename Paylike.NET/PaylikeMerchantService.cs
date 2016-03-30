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
using Paylike.NET.ResponseModels.Apps;
using Paylike.NET.ResponseModels.Merchants;

namespace Paylike.NET
{
    public class PaylikeMerchantService : BaseService, IPaylikeMerchantService
    {
        public PaylikeMerchantService(string privateApiKey): base(privateApiKey)
        {

        }

        public ApiResponse<object> AddAppToMerchant(AddAppToMerchantRequest request)
        {
            return SendApiRequest<AddAppToMerchantRequest, object>(request);
        }

        public ApiResponse<object> RevokeAppFromMerchant(RevokeAppFromMerchantRequest request)
        {
            return SendApiRequest<RevokeAppFromMerchantRequest, object>(request);
        }

        public ApiResponse<Merchant> CreateMerchant(CreateMerchantRequest request)
        {
            return SendApiRequest<CreateMerchantRequest, Merchant>(request);
        }

        public ApiResponse<Merchant> GetMerchant(GetMerchantRequest request)
        {
            return SendApiRequest<GetMerchantRequest, Merchant>(request);
        }

        public ApiResponse<List<Merchant>> GetMerchants(GetMerchantsRequest request)
        {
            return SendApiRequest<GetMerchantsRequest, List<Merchant>>(request);
        }

        public ApiResponse<object> UpdateMerchant(UpdateMerchantRequest request)
        {
            return SendApiRequest<UpdateMerchantRequest, object>(request);
        }

        public ApiResponse<InviteUserToMerchantResponse> InviteUserToMerchant(InviteUserToMerchantRequest request)
        {
            return SendApiRequest<InviteUserToMerchantRequest, InviteUserToMerchantResponse>(request);
        }

        public ApiResponse<object> RevokeUserFromMerchant(RevokeUserFromMerchantRequest request)
        {
            return SendApiRequest<RevokeUserFromMerchantRequest, object>(request);
        }

        public ApiResponse<List<User>> GetMerchantUsers(GetMerchantUsersRequest request)
        {
            return SendApiRequest<GetMerchantUsersRequest, List<User>>(request);
        }
        public ApiResponse<List<App>> GetMerchantApps(GetMerchantAppsRequest request)
        {
            return SendApiRequest<GetMerchantAppsRequest, List<App>>(request);
        }

        public ApiResponse<List<Line>> GetMerchantLines(GetMerchantLinesRequest request)
        {
            return SendApiRequest<GetMerchantLinesRequest, List<Line>>(request);
        }

        public ApiResponse<Card> SaveCard(SaveCardRequest request)
        {
            return SendApiRequest<SaveCardRequest, Card>(request);
        }

        protected override string ProcessApiResponse(string json, string requestName)
        {
            string processedJson = json;
            switch(requestName)
            {
                case "CreateMerchant":
                    {
                        processedJson = JObject.Parse(json).SelectToken("merchant").ToString();
                        break;
                    }
                case "GetMerchant":
                    {
                        processedJson = JObject.Parse(json).SelectToken("merchant").ToString();
                        break;
                    }
                case "SaveCard":
                    {
                        processedJson = JObject.Parse(json).SelectToken("card").ToString();
                        break;
                    }
            }

            return processedJson;
        }
    }
}
