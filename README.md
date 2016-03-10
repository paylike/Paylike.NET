# Paylike.NET
.NET client for the Paylike API

# Avaiable services:

**PaylikeAppService**

Methods:
* ApiResponse<CreateAppResponse> CreateApp(CreateAppRequest request);
* ApiResponse<GetCurrentAppResponse> GetCurrentApp();
* ApiResponse<object> AddAppToMerchant(AddAppToMerchantRequest request);
* void SetApiKey(string privateApiKey);
