using Paylike.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paylike.NET.RequestModels.Transactions;
using Paylike.NET.ResponseModels;
using Newtonsoft.Json.Linq;
using Paylike.NET.ResponseModels.Transactions;
using Paylike.NET.Entities;

namespace Paylike.NET
{
    public class PaylikeTransactionService : BaseService, IPaylikeTransactionService
    {
        public PaylikeTransactionService(string privateKey) :
            base(privateKey)
        {

        }

        public ApiResponse<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request)
        {
            return SendApiRequest<CreateTransactionRequest, CreateTransactionResponse>(request);
        }

        public ApiResponse<Transaction> CaptureTransaction(CaptureTransactionRequest request)
        {
            return SendApiRequest<CaptureTransactionRequest, Transaction>(request);
        }

        public ApiResponse<Transaction> RefundTransaction(RefundTransactionRequest request)
        {
            return SendApiRequest<RefundTransactionRequest, Transaction>(request);
        }

        public ApiResponse<Transaction> VoidTransaction(VoidTransactionRequest request)
        {
            return SendApiRequest<VoidTransactionRequest, Transaction>(request);
        }

        public ApiResponse<Transaction> GetTransaction(GetTransactionRequest request)
        {
            return SendApiRequest<GetTransactionRequest, Transaction>(request);
        }
        public ApiResponse<List<Transaction>> GetTransactions(GetTransactionsRequest request)
        {
            return SendApiRequest<GetTransactionsRequest, List<Transaction>>(request);
        }

        protected override string ProcessApiResponse(string json, string requestName)
        {
            string processedJson = json;
            switch (requestName)
            {
                case "CreateTransaction":
                case "CaptureTransaction":
                case "RefundTransaction":
                case "VoidTransaction":
                case "GetTransaction":
                    {
                        processedJson = JObject.Parse(json).SelectToken("transaction").ToString();
                        break;
                    }
            }

            return processedJson;
        }

    }
}
