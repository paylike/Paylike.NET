using Paylike.NET.Entities;
using Paylike.NET.RequestModels.Transactions;
using Paylike.NET.ResponseModels;
using Paylike.NET.ResponseModels.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Interfaces
{
    public interface IPaylikeTransactionService
    {
        ApiResponse<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request);

        ApiResponse<Transaction> CaptureTransaction(CaptureTransactionRequest request);

        ApiResponse<Transaction> RefundTransaction(RefundTransactionRequest request);

        ApiResponse<Transaction> VoidTransaction(VoidTransactionRequest request);

        ApiResponse<Transaction> GetTransaction(GetTransactionRequest request);

        ApiResponse<List<Transaction>> GetTransactions(GetTransactionsRequest request);
    }
}
