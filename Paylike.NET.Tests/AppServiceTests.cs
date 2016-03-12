using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paylike.NET.Interfaces;
using Paylike.NET.RequestModels.Apps;
using Paylike.NET.ResponseModels.Apps;
using Paylike.NET.ResponseModels;
using Paylike.NET.Entities;
using Paylike.NET.RequestModels.Merchants;
using Paylike.NET.Constants;

namespace Paylike.NET.Tests
{
    [TestClass]
    public class AppServiceTests
    {
        private IPaylikeAppService _appService;

        [TestInitialize]
        public void TestInitialize()
        {
            _appService = new PaylikeAppService(string.Empty);
        }

        [TestMethod]
        public void CreateApp_Success()
        {
            CreateAppRequest request = new CreateAppRequest()
            {
                Name = "AppFromApiTest_" + DateTime.Now.Ticks.ToString()
            };

            ApiResponse<App> response = _appService.CreateApp(request);
            
            Assert.IsNotNull(response.Content);

            App createdApp = response.Content;

            Assert.AreEqual(request.Name, createdApp.Name);
        }

        [TestMethod]
        public void GetCurrentApp_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content;
            _appService.SetApiKey(createdApp.Key);

            GetCurrentAppResponse getAppResponse = _appService.GetCurrentApp().Content;

            Assert.AreEqual(createdApp.Name, getAppResponse.Identity.Name);
            Assert.AreEqual(createdApp.Id, getAppResponse.Identity.Id);
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

            Assert.AreEqual(merchantName, merchant.Name);
            Assert.IsFalse(string.IsNullOrEmpty(merchant.Id));

            AddAppToMerchantRequest addRequest = new AddAppToMerchantRequest()
            {
                MerchanId = merchant.Id,
                AppId = createdApp.Id
            };

            var addResponse = _appService.AddAppToMerchant(addRequest);

            Assert.IsNull(addResponse.Content);
            Assert.IsFalse(addResponse.IsError);
        }


    }
}
