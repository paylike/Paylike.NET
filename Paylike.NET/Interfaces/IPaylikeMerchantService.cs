using Paylike.NET.Entities;
using Paylike.NET.RequestModels.Apps;
using Paylike.NET.RequestModels.Merchants;
using Paylike.NET.ResponseModels;
using Paylike.NET.ResponseModels.Apps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Interfaces
{
    public interface IPaylikeMerchantService
    {
        ApiResponse<object> AddAppToMerchant(AddAppToMerchantRequest request);

        ApiResponse<object> RevokeAppFromMerchant(RevokeAppFromMerchantRequest request);

        ApiResponse<Merchant> CreateMerchant(CreateMerchantRequest request);

        ApiResponse<object> UpdateMerchant(UpdateMerchantRequest request);

        ApiResponse<Merchant> GetMerchant(GetMerchantRequest request);

        ApiResponse<List<Merchant>> GetMerchants(GetMerchantsRequest request);

        ApiResponse<InviteUserToMerchantResponse> InviteUserToMerchant(InviteUserToMerchantRequest request);
    }
}
