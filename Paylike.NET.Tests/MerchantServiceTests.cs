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

        [TestInitialize]
        public void TestInitialize()
        {
            _appService = new PaylikeAppService(string.Empty);
        }


        [TestMethod]
        public void AddAppToMerchant_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            _appService.SetApiKey(createdApp.Key);

            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);

            string merchantName = "TestMerchant_" + DateTime.Now.Ticks.ToString();
            Merchant merchant = merchantService.CreateMerchant(new CreateMerchantRequest()
            {
                Name = merchantName,
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
            }).Content;

            AddAppToMerchantRequest addRequest = new AddAppToMerchantRequest()
            {
                MerchanId = merchant.Id,
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

            string merchantName = "TestMerchant_" + DateTime.Now.Ticks.ToString();
            Merchant merchant = merchantService.CreateMerchant(new CreateMerchantRequest()
            {
                Name = merchantName,
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
            }).Content;

            AddAppToMerchantRequest addRequest = new AddAppToMerchantRequest()
            {
                MerchanId = merchant.Id,
                AppId = createdApp.Id
            };

            merchantService.AddAppToMerchant(addRequest);
            var revokeResponse = merchantService.RevokeAppFromMerchant(new RevokeAppFromMerchantRequest()
            {
                MerchanId = merchant.Id,
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

            string merchantName = "TestMerchant_" + DateTime.Now.Ticks.ToString();
            Merchant merchant = merchantService.CreateMerchant(new CreateMerchantRequest()
            {
                Name = merchantName,
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
            }).Content;

            Assert.AreEqual(merchantName, merchant.Name);
            Assert.IsFalse(string.IsNullOrEmpty(merchant.Id));
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

            string merchantName = "TestMerchant_" + DateTime.Now.Ticks.ToString();
            Merchant createdMerchant = merchantService.CreateMerchant(new CreateMerchantRequest()
            {
                Name = merchantName,
                Currency = Currency.EUR,
                Test = true,
                Email = "test@gmail.com",
                Website = "test.com",
                Descriptor = "descriptor",
                Company = new Company()
                {
                    Country = Country.Austria,
                    Number = "NUMBER123"
                },
                Bank = new Bank()
                {
                    IBAN = "NL18ABNA0484869868"
                }
            }).Content;

            Merchant gotMerchant = merchantService.GetMerchant(new GetMerchantRequest() { MerchanId = createdMerchant.Id }).Content;


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
                merchantService.CreateMerchant(new CreateMerchantRequest()
                {
                    Name = "TestMerchant_" + DateTime.Now.Ticks.ToString(),
                    Currency = Currency.EUR,
                    Test = true,
                    Email = "test@gmail.com",
                    Website = "test.com",
                    Descriptor = "descriptor",
                    Company = new Company()
                    {
                        Country = Country.Austria,
                        Number = "NUMBER123"
                    },
                    Bank = new Bank()
                    {
                        IBAN = "NL18ABNA0484869868"
                    }
                });
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

            string merchantName = "TestMerchant_" + DateTime.Now.Ticks.ToString();
            Merchant createdMerchant = merchantService.CreateMerchant(new CreateMerchantRequest()
            {
                Name = merchantName,
                Currency = Currency.EUR,
                Test = true,
                Email = "test@gmail.com",
                Website = "test.com",
                Descriptor = "descriptor",
                Company = new Company()
                {
                    Country = Country.Austria,
                    Number = "NUMBER123"
                },
                Bank = new Bank()
                {
                    IBAN = "NL18ABNA0484869868"
                }
            }).Content;

            merchantService.UpdateMerchant(new UpdateMerchantRequest() {
                MerchanId = createdMerchant.Id,
                Email = "new_email@test.com",
                Name = "new_name",
                Descriptor = "newDesc"
            });

            Merchant gotMerchant = merchantService.GetMerchant(new GetMerchantRequest() { MerchanId = createdMerchant.Id }).Content;

            Assert.AreEqual(gotMerchant.Name, "new_name");
            Assert.AreEqual(gotMerchant.Email, "new_email@test.com");
            Assert.AreEqual(gotMerchant.Descriptor, "newDesc");
        }

        [TestMethod]
        [Ignore]
        public void InviteUserToMerhant_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            IPaylikeMerchantService merchantService = new PaylikeMerchantService(createdApp.Key);

            string merchantName = "TestMerchant_" + DateTime.Now.Ticks.ToString();
            Merchant createdMerchant = merchantService.CreateMerchant(new CreateMerchantRequest()
            {
                Name = merchantName,
                Currency = Currency.EUR,
                Test = true,
                Email = "test@gmail.com",
                Website = "test.com",
                Descriptor = "descriptor",
                Company = new Company()
                {
                    Country = Country.Austria,
                    Number = "NUMBER123"
                },
                Bank = new Bank()
                {
                    IBAN = "NL18ABNA0484869868"
                }
            }).Content;

            var inviteResult = merchantService.InviteUserToMerchant(new InviteUserToMerchantRequest()
            {
                MerchanId = createdMerchant.Id,
                Email = "test@test.com"
            });

            Assert.IsNotNull(inviteResult.Content);
            Assert.IsFalse(inviteResult.IsError);
            Assert.AreEqual(201, inviteResult.ResponseCode);
        }
    }
}
