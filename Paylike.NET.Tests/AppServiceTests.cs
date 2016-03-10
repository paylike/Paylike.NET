using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paylike.NET.Interfaces;
using Paylike.NET.RequestModels.Apps;
using Paylike.NET.ResponseModels.Apps;
using Paylike.NET.ResponseModels;
using Paylike.NET.Entities;

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

            ApiResponse<CreateAppResponse> response = _appService.CreateApp(request);
            
            Assert.IsNotNull(response.Content);

            App createdApp = response.Content.App;

            Assert.AreEqual(request.Name, createdApp.Name);
        }

        [TestMethod]
        public void GetCurrentApp_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content.App;
            _appService.SetApiKey(createdApp.Key);

            GetCurrentAppResponse getAppResponse = _appService.GetCurrentApp().Content;

            Assert.AreEqual(createdApp.Name, getAppResponse.Identity.Name);
            Assert.AreEqual(createdApp.Id, getAppResponse.Identity.Id);
        }

        [TestMethod]
        public void CreateAppAndAssignToMerchant_Success()
        {
            App createdApp = _appService.CreateApp(new CreateAppRequest()).Content.App;
            _appService.SetApiKey(createdApp.Key);

            AddAppToMerchantRequest addRequest = new AddAppToMerchantRequest() {
                MerchanId = "56dc7d44dec8ce670fbc3448",
                AppId = createdApp.Id
            };

            var addResponse = _appService.AddAppToMerchant(addRequest);

            Assert.IsNull(addResponse.Content);
            Assert.IsFalse(addResponse.IsError);
        }
    }
}
