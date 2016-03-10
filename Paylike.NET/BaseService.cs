using Newtonsoft.Json;
using Paylike.NET.RequestModels;
using Paylike.NET.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET
{
    public abstract class BaseService
    {
        private string EndpointBase = @"https://api.paylike.io";
        protected HttpClient _apiClient { get; set; }

        public BaseService(string privateApiKey)
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(EndpointBase);
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(privateApiKey))
            {
                _apiClient.DefaultRequestHeaders.Add("Authorization", GetAuthorizationHeaderValue(privateApiKey));
            }

            _apiClient.DefaultRequestHeaders.Add("User-agent", string.Format("Paylike.NET {0}", typeof(BaseService).Assembly.GetName().Version.ToString()));
        }

        protected ApiResponse<ResponseType> SendApiRequest<RequestType, ResponseType>(RequestType request)
            where RequestType : RequestBase, new()
            where ResponseType : class, new()

        {
            ApiResponse<ResponseType> apiResponse = new ApiResponse<ResponseType>();
            HttpResponseMessage httpResponse = null;

            switch (request.HttpMethod)
            {
                case "POST":
                    {
                        HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        httpResponse = _apiClient.PostAsync(request.Uri, contentPost).Result;
                        break;
                    }
                case "GET":
                    {
                        httpResponse = _apiClient.GetAsync(request.Uri).Result;
                        break;
                    }
                case "PUT":
                    {
                        HttpContent contentPut = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        httpResponse = _apiClient.PutAsync(request.Uri, contentPut).Result;
                        break;
                    }
            }

            if (httpResponse != null)
            {
                string jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;
                if (httpResponse.IsSuccessStatusCode)
                {
                    apiResponse.Content = JsonConvert.DeserializeObject<ResponseType>(jsonResponse);
                }
                else
                {
                    
                }
            }

            return apiResponse;
        }

        protected string GetAuthorizationHeaderValue(string privateApiKey)
        {
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(":{0}", privateApiKey)));
            return string.Format("Basic {0}", token);
        }
    }
}
