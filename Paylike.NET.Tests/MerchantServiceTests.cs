using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paylike.NET.Constants;
using Paylike.NET.Entities;
using Paylike.NET.Interfaces;
using Paylike.NET.RequestModels.Apps;
using Paylike.NET.RequestModels.Merchants;
using Paylike.NET.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Tests
{
    [TestClass]
    public class MerchantServiceTests
    {
        private IPaylikeAppService _appService;
        private CreateMerchantRequest _createMerchantRequest;
        public string AppKey = "02a738b2-d0bf-43dc-b047-31b45cb86bbb";
        public string MerchantId = "56dc7d44dec8ce670fbc3448";
        public string TransactionId = "56f975ce0b692dac1a58c124";
        public string CardId = "56fc165d32ea1415191fc7b8";

        [TestInitialize]
        public void TestInitialize()
        {
            _appService = new PaylikeAppService(string.Empty);
            _createMerchantRequest = new CreateMerchantRequest()
            {
                Name = "TestMerchant_" + DateTime.Now.Ticks.ToString(),
                Currency = Currency.EUR,
                Test = true,
                Email = "test@gmail.com",
                Website = "test.com",
                Descriptor = "descriptor",
                Company = new Company()
                {
                    Country = Country.Austria
                },
                Bank = new Bank()
                {
                    IBAN = "NL18ABNA0484869868"
                }
            };
        }

        [TestMethod]
        public void AddAppToMerchant_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            _appService.SetApiKey(createdApp.Key);

            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);
            Merchant merchant = merchantService.CreateMerchant(_createMerchantRequest).Content;

            AddAppToMerchantRequest addRequest = new AddAppToMerchantRequest()
            {
                MerchantId = merchant.Id,
                AppId = createdApp.Id
            };

            var addResponse = merchantService.AddAppToMerchant(addRequest);

            Assert.IsNull(addResponse.Content);
            Assert.IsFalse(addResponse.IsError);
            Assert.AreEqual(201, addResponse.ResponseCode);
        }

        [TestMethod]
        public void RevokeAppFromMerchant_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            _appService.SetApiKey(createdApp.Key);

            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);
            Merchant merchant = merchantService.CreateMerchant(_createMerchantRequest).Content;

            AddAppToMerchantRequest addRequest = new AddAppToMerchantRequest()
            {
                MerchantId = merchant.Id,
                AppId = createdApp.Id
            };

            merchantService.AddAppToMerchant(addRequest);
            var revokeResponse = merchantService.RevokeAppFromMerchant(new RevokeAppFromMerchantRequest()
            {
                MerchantId = merchant.Id,
                AppId = createdApp.Id
            });

            Assert.IsNull(revokeResponse.Content);
            Assert.IsFalse(revokeResponse.IsError);
            Assert.AreEqual(204, revokeResponse.ResponseCode);
        }

        [TestMethod]
        public void CreateMerchant_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);

            ApiResponse<Merchant> merchant = merchantService.CreateMerchant(_createMerchantRequest);

            Assert.AreEqual(201, merchant.ResponseCode);
            Assert.IsFalse(string.IsNullOrEmpty(merchant.Content.Id));
        }

        [TestMethod]
        public void CreateMerchant_WhenMissingRequiredFields_Fails()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);

            ApiResponse<Merchant> response = merchantService.CreateMerchant(new CreateMerchantRequest()
            {
                Test = true
            });

            Assert.IsTrue(response.IsError);
            Assert.IsNotNull(response.ErrorContent);
            Assert.AreEqual(400, response.ResponseCode);
        }

        [TestMethod]
        public void GetMerchant_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);

            Merchant createdMerchant = merchantService.CreateMerchant(_createMerchantRequest).Content;

            Merchant gotMerchant = merchantService.GetMerchant(new GetMerchantRequest() { MerchantId = createdMerchant.Id }).Content;


            Assert.AreEqual(gotMerchant.Name, createdMerchant.Name);
            Assert.AreEqual(gotMerchant.Currency, createdMerchant.Currency);
            Assert.AreEqual(gotMerchant.Test, createdMerchant.Test);
            Assert.AreEqual(gotMerchant.Email, createdMerchant.Email);
            Assert.AreEqual(gotMerchant.Website, createdMerchant.Website);
            Assert.AreEqual(gotMerchant.Descriptor, createdMerchant.Descriptor);
            Assert.AreEqual(gotMerchant.Company.Country, createdMerchant.Company.Country);
            Assert.AreEqual(gotMerchant.Company.Number, createdMerchant.Company.Number);
            Assert.AreEqual(gotMerchant.Bank.IBAN, createdMerchant.Bank.IBAN);
        }

        [TestMethod]
        public void GetMerchants_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);

            for(int i = 0; i < 5; i++)
            {
                _createMerchantRequest.Name = "TestMerchant_" + DateTime.Now.Ticks.ToString();
                merchantService.CreateMerchant(_createMerchantRequest);
            }

            ApiResponse<List<Merchant>> merchantsResponse = merchantService.GetMerchants(new GetMerchantsRequest() {
                AppId = createdApp.Id,
                Limit = 3
            });

            Assert.AreEqual(3, merchantsResponse.Content.Count);

            var beforeMerchants = merchantService.GetMerchants(new GetMerchantsRequest()
            {
                AppId = createdApp.Id,
                Before = merchantsResponse.Content[2].Id,
                Limit = 2
            });

            Assert.AreEqual(2, beforeMerchants.Content.Count);

            var afterMerchants = merchantService.GetMerchants(new GetMerchantsRequest()
            {
                AppId = createdApp.Id,
                After = merchantsResponse.Content[2].Id,
                Limit = 2
            });

            Assert.AreEqual(2, beforeMerchants.Content.Count);

            var firstMerchantsIds = merchantsResponse.Content.Select(m => m.Id);
            var beforeMerchantsIds = beforeMerchants.Content.Select(m => m.Id);
            var afterMerchantsIds = afterMerchants.Content.Select(m => m.Id);

            var beforeIntersection = firstMerchantsIds.Intersect(beforeMerchantsIds);
            var afterIntersection = firstMerchantsIds.Intersect(afterMerchantsIds);

            Assert.AreEqual(0, beforeIntersection.Count());
            Assert.AreEqual(2, afterIntersection.Count());
        }

        [TestMethod]
        public void GetMerchants_WithoutPagination_Fails()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);

            ApiResponse<List<Merchant>> merchantsResponse = merchantService.GetMerchants(new GetMerchantsRequest()
            {
                AppId = createdApp.Id
            });

            Assert.IsTrue(merchantsResponse.IsError);
            Assert.IsNotNull(merchantsResponse.ErrorContent);
            Assert.AreEqual(400, merchantsResponse.ResponseCode);
        }

        [TestMethod]
        public void UpdateMerchant_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);
            Merchant createdMerchant = merchantService.CreateMerchant(_createMerchantRequest).Content;

            merchantService.UpdateMerchant(new UpdateMerchantRequest() {
                MerchantId = createdMerchant.Id,
                Email = "new_email@test.com",
                Name = "new_name",
                Descriptor = "newDesc"
            });

            Merchant gotMerchant = merchantService.GetMerchant(new GetMerchantRequest() { MerchantId = createdMerchant.Id }).Content;

            Assert.AreEqual(gotMerchant.Name, "new_name");
            Assert.AreEqual(gotMerchant.Email, "new_email@test.com");
            Assert.AreEqual(gotMerchant.Descriptor, "newDesc");
        }

        [TestMethod]
        public void InviteUserToMerhant_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);
            Merchant createdMerchant = merchantService.CreateMerchant(_createMerchantRequest).Content;

            var inviteResult = merchantService.InviteUserToMerchant(new InviteUserToMerchantRequest()
            {
                MerchantId = createdMerchant.Id,
                Email = "test@example.com"
            });

            Assert.IsNotNull(inviteResult.Content);
            Assert.IsFalse(inviteResult.IsError);
            Assert.AreEqual(201, inviteResult.ResponseCode);
        }

        [TestMethod]
        public void RevokeUserFromMerhant_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);
            Merchant createdMerchant = merchantService.CreateMerchant(_createMerchantRequest).Content;

            merchantService.InviteUserToMerchant(new InviteUserToMerchantRequest()
            {
                MerchantId = createdMerchant.Id,
                Email = "test@example.com"
            });

            var merchantUsers = merchantService.GetMerchantUsers(new GetMerchantUsersRequest()
            {
                MerchantId = createdMerchant.Id,
                Limit = 5
            });

            var revokeResponse = merchantService.RevokeUserFromMerchant(new RevokeUserFromMerchantRequest() {
                MerchantId = createdMerchant.Id,
                UserId = merchantUsers.Content[0].Id
            });

            Assert.IsNull(revokeResponse.Content);
            Assert.IsFalse(revokeResponse.IsError);
            Assert.AreEqual(204, revokeResponse.ResponseCode);
        }

        [TestMethod]
        public void GetMerchantUsers_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);

            var createdMerchant = merchantService.CreateMerchant(_createMerchantRequest).Content;
            for (int i = 0; i < 5; i++)
            {
                merchantService.InviteUserToMerchant(new InviteUserToMerchantRequest()
                {
                    MerchantId = createdMerchant.Id,
                    Email = "test" + i.ToString() + "@example.com"
                });
            }

            ApiResponse<List<User>> usersResponse = merchantService.GetMerchantUsers(new GetMerchantUsersRequest()
            {
                MerchantId = createdMerchant.Id,
                Limit = 3
            });

            Assert.AreEqual(3, usersResponse.Content.Count);

            var beforeUsers = merchantService.GetMerchantUsers(new GetMerchantUsersRequest()
            {
                MerchantId = createdMerchant.Id,
                Before = usersResponse.Content[2].Id,
                Limit = 2
            });

            Assert.AreEqual(2, beforeUsers.Content.Count);

            var afterMerchants = merchantService.GetMerchantUsers(new GetMerchantUsersRequest()
            {
                MerchantId = createdMerchant.Id,
                After = usersResponse.Content[2].Id,
                Limit = 2
            });

            Assert.AreEqual(2, beforeUsers.Content.Count);

            var firtUsersIds = usersResponse.Content.Select(m => m.Id);
            var beforeUsersIds = beforeUsers.Content.Select(m => m.Id);
            var afterUsersIds = afterMerchants.Content.Select(m => m.Id);

            var beforeIntersection = firtUsersIds.Intersect(beforeUsersIds);
            var afterIntersection = firtUsersIds.Intersect(afterUsersIds);

            Assert.AreEqual(0, beforeIntersection.Count());
            Assert.AreEqual(2, afterIntersection.Count());
        }

        [TestMethod]
        public void GetMerchantApps_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            _appService.SetApiKey(createdApp.Key);

            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);
            Merchant merchant = merchantService.CreateMerchant(_createMerchantRequest).Content;

            for (int i = 0; i < 4; i++)
            {
                createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
                merchantService.AddAppToMerchant(new AddAppToMerchantRequest()
                {
                    MerchantId = merchant.Id,
                    AppId = createdApp.Id
                });
            }

            ApiResponse<List<App>> appsResponse = merchantService.GetMerchantApps(new GetMerchantAppsRequest()
            {
                MerchantId = merchant.Id,
                Limit = 3
            });

            Assert.AreEqual(3, appsResponse.Content.Count);

            var beforeApps = merchantService.GetMerchantApps(new GetMerchantAppsRequest()
            {
                MerchantId = merchant.Id,
                Before = appsResponse.Content[2].Id,
                Limit = 2
            });

            Assert.AreEqual(2, beforeApps.Content.Count);

            var afterApps = merchantService.GetMerchantApps(new GetMerchantAppsRequest()
            {
                MerchantId = merchant.Id,
                After = appsResponse.Content[2].Id,
                Limit = 2
            });

            Assert.AreEqual(2, beforeApps.Content.Count);

            var firstAppsIds = appsResponse.Content.Select(m => m.Id);
            var beforeAppsIds = beforeApps.Content.Select(m => m.Id);
            var afterAppsIds = afterApps.Content.Select(m => m.Id);

            var beforeIntersection = firstAppsIds.Intersect(beforeAppsIds);
            var afterIntersection = firstAppsIds.Intersect(afterAppsIds);

            Assert.AreEqual(0, beforeIntersection.Count());
            Assert.AreEqual(2, afterIntersection.Count());
        }

        [TestMethod]
        public void GetMerchantLines_Success()
        {
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(AppKey);
           
            ApiResponse<List<Line>> linesResponse = merchantService.GetMerchantLines(new GetMerchantLinesRequest()
            {
                MerchantId = MerchantId,
                Limit = 3
            });
            Assert.AreEqual(3, linesResponse.Content.Count);

            var beforeLines = merchantService.GetMerchantLines(new GetMerchantLinesRequest()
            {
                MerchantId = MerchantId,
                Before = linesResponse.Content[2].Id,
                Limit = 2
            });

            Assert.AreEqual(2, beforeLines.Content.Count);

            var afterLines = merchantService.GetMerchantLines(new GetMerchantLinesRequest()
            {
                MerchantId = MerchantId,
                After = linesResponse.Content[2].Id,
                Limit = 2
            });

            Assert.AreEqual(2, afterLines.Content.Count);

            var firstLinesIds = linesResponse.Content.Select(m => m.Id);
            var beforeLinesIds = beforeLines.Content.Select(m => m.Id);
            var afterAppsIds = afterLines.Content.Select(m => m.Id);

            var beforeIntersection = firstLinesIds.Intersect(beforeLinesIds);
            var afterIntersection = firstLinesIds.Intersect(afterAppsIds);

            Assert.AreEqual(0, beforeIntersection.Count());
            Assert.AreEqual(2, afterIntersection.Count());
        }

        [TestMethod]
        public void SaveCard_Success()
        {
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(AppKey);
            var cardResponse = merchantService.SaveCard(new SaveCardRequest() { MerchantId = MerchantId, TransactionId = TransactionId });

            Assert.IsNotNull(cardResponse.Content.Id);
            Assert.IsNotNull(cardResponse.Content);
            Assert.IsFalse(cardResponse.IsError);
            Assert.AreEqual(201, cardResponse.ResponseCode);
        }

        [TestMethod]
        public void FetchCard_Success()
        {
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(AppKey);
            var cardResponse = merchantService.FetchCard(new FetchCardRequest() { CardID = CardId });

            Assert.IsNotNull(cardResponse.Content.Id);
            Assert.IsNotNull(cardResponse.Content);
            Assert.IsFalse(cardResponse.IsError);
            Assert.AreEqual(200, cardResponse.ResponseCode);
        }
    }
}
