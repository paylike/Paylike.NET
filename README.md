# Paylike.NET
.NET client for the Paylike API

# Avaiable services:

**PaylikeAppService**

Methods:
* ApiResponse<App> CreateApp(CreateAppRequest request);
* ApiResponse<GetCurrentAppResponse> GetCurrentApp();
* void SetApiKey(string privateApiKey);

**PaylikeMerchantService**

Methods:
* ApiResponse<object> AddAppToMerchant(AddAppToMerchantRequest request);
* ApiResponse<object> RevokeAppFromMerchant(RevokeAppFromMerchantRequest request);
* ApiResponse<Merchant> CreateMerchant(CreateMerchantRequest request);
* ApiResponse<object> UpdateMerchant(UpdateMerchantRequest request);
* ApiResponse<Merchant> GetMerchant(GetMerchantRequest request);
* ApiResponse<List<Merchant>> GetMerchants(GetMerchantsRequest request);
* ApiResponse<InviteUserToMerchantResponse> InviteUserToMerchant(InviteUserToMerchantRequest request);
* ApiResponse<object> RevokeUserFromMerchant(RevokeUserFromMerchantRequest request);
* ApiResponse<List<User>> GetMerchantUsers(GetMerchantUsersRequest request);
* ApiResponse<List<App>> GetMerchantApps(GetMerchantAppsRequest request);
* ApiResponse<List<Line>> GetMerchantLines(GetMerchantLinesRequest request);
* ApiResponse<Card> SaveCard(SaveCardRequest request);

**PaylikeTransactionService**

* ApiResponse<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request);
* ApiResponse<Transaction> CaptureTransaction(CaptureTransactionRequest request);
* ApiResponse<Transaction> RefundTransaction(RefundTransactionRequest request);
* ApiResponse<Transaction> VoidTransaction(VoidTransactionRequest request);
* ApiResponse<Transaction> GetTransaction(GetTransactionRequest request);
* ApiResponse<List<Transaction>> GetTransactions(GetTransactionsRequest request);
