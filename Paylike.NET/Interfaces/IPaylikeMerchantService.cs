using Paylike.NET.Entities;
using Paylike.NET.RequestModels.Merchants;
using Paylike.NET.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Interfaces
{
    public interface IPaylikeMerchantService
    {
        ApiResponse<Merchant> CreateMerchant(CreateMerchantRequest request);

        ApiResponse<object> UpdateMerchant(UpdateMerchantRequest request);

        ApiResponse<Merchant> GetMerchant(GetMerchantRequest request);
    }
}
