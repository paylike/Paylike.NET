using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paylike.NET.Constants;
using Paylike.NET.Entities;
using Paylike.NET.Interfaces;
using Paylike.NET.RequestModels.Merchants;
using Paylike.NET.RequestModels.Transactions;
using Paylike.NET.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylike.NET.Tests
{
    [TestClass]
    public class TransactionServiceTests
    {
        public string TransactionId = "56f975ce0b692dac1a58c124";
        public string AppKey = "02a738b2-d0bf-43dc-b047-31b45cb86bbb";
        public string MerchantId = "56dc7d44dec8ce670fbc3448";
        private CreateTransactionRequest createTransactionRequest;
        private IPaylikeTransactionService transactionService;

        [TestInitialize]
        public void TestInitialize()
        {
            transactionService = new PaylikeTransactionService(AppKey);
            createTransactionRequest = new CreateTransactionRequest()
            {
                Amount = 200,
                Currency = Currency.RON,
                Descriptor = "customDescriptor",
                TransactionId = TransactionId,
                MerchantId = MerchantId,
            };
        }

        [TestMethod]
        public void CreateTransaction_UsingPreviousTranscactionId_Success()
        {
            var response = transactionService.CreateTransaction(createTransactionRequest);

            Assert.IsFalse(response.IsError);
            Assert.AreEqual(201, response.ResponseCode);
            Assert.IsNotNull(response.Content.Id);
        }

        [TestMethod]
        public void GetTransaction_Success()
        {
            var response = transactionService.CreateTransaction(createTransactionRequest);
            var getTransactionResult = transactionService.GetTransaction(new GetTransactionRequest() { TransactionId = response.Content.Id });


            Assert.IsFalse(getTransactionResult.IsError);
            Assert.AreEqual(200, getTransactionResult.ResponseCode);
            Assert.AreEqual(response.Content.Id, getTransactionResult.Content.Id);
        }

        [TestMethod]
        public void GetTransactions_Success()
        {
            for(var i = 0; i < 5; i++)
                transactionService.CreateTransaction(createTransactionRequest);

            ApiResponse<List<Transaction>> transactionsResponse = transactionService.GetTransactions(new GetTransactionsRequest()
            {
                MerchantId = MerchantId,
                Limit = 3
            });

            Assert.AreEqual(3, transactionsResponse.Content.Count);

            var beforeTransactions = transactionService.GetTransactions(new GetTransactionsRequest()
            {
                MerchantId = MerchantId,
                Before = transactionsResponse.Content[2].Id,
                Limit = 2
            });

            Assert.AreEqual(2, beforeTransactions.Content.Count);

            var afterTransactions = transactionService.GetTransactions(new GetTransactionsRequest()
            {
                MerchantId = MerchantId,
                After = transactionsResponse.Content[2].Id,
                Limit = 2
            });

            Assert.AreEqual(2, afterTransactions.Content.Count);

            var firstTransactionIds = transactionsResponse.Content.Select(m => m.Id);
            var beforeTransactionIds = beforeTransactions.Content.Select(m => m.Id);
            var afterTransactionIds = afterTransactions.Content.Select(m => m.Id);

            var beforeIntersection = firstTransactionIds.Intersect(beforeTransactionIds);
            var afterIntersection = firstTransactionIds.Intersect(afterTransactionIds);

            Assert.AreEqual(0, beforeIntersection.Count());
            Assert.AreEqual(2, afterIntersection.Count());
        }

        [TestMethod]
        public void CaptureTransaction_Success()
        {
            var response = transactionService.CreateTransaction(createTransactionRequest);

            CaptureTransactionRequest captureRequest = new CaptureTransactionRequest()
            {
                 Amount = createTransactionRequest.Amount,
                 Currency = createTransactionRequest.Currency,
                 TransactionId = response.Content.Id  
            };

            var captureResponse = transactionService.CaptureTransaction(captureRequest);
            var transaction = captureResponse.Content;

            Assert.IsFalse(captureResponse.IsError);
            Assert.AreEqual(201, captureResponse.ResponseCode);
            Assert.IsNotNull(transaction.Id);
            Assert.AreEqual(createTransactionRequest.Amount, transaction.Amount);
            Assert.AreEqual(createTransactionRequest.Currency, transaction.Currency);
            Assert.AreEqual(createTransactionRequest.Amount, transaction.CapturedAmount);
            Assert.IsTrue(transaction.Trail[0].Capture);
        }

        [TestMethod]
        public void RefundTransaction_FullRefund_Success()
        {
            var response = transactionService.CreateTransaction(createTransactionRequest);

            CaptureTransactionRequest captureRequest = new CaptureTransactionRequest()
            {
                Amount = createTransactionRequest.Amount,
                Currency = createTransactionRequest.Currency,
                TransactionId = response.Content.Id
            };

            var captureResponse = transactionService.CaptureTransaction(captureRequest);

            var refundRequest = new RefundTransactionRequest()
            {
                TransactionId = captureResponse.Content.Id,
                Amount = captureResponse.Content.Amount
            };

            var refundResponse = transactionService.RefundTransaction(refundRequest);

            var transaction = refundResponse.Content;

            Assert.IsFalse(refundResponse.IsError);
            Assert.AreEqual(201, refundResponse.ResponseCode);
            Assert.IsNotNull(transaction.Id);
            Assert.AreEqual(createTransactionRequest.Amount, transaction.RefundedAmount);
            Assert.AreEqual(createTransactionRequest.Amount, transaction.CapturedAmount);
        }

        [TestMethod]
        public void RefundTransaction_PartialRefund_Success()
        {
            var response = transactionService.CreateTransaction(createTransactionRequest);

            CaptureTransactionRequest captureRequest = new CaptureTransactionRequest()
            {
                Amount = createTransactionRequest.Amount,
                Currency = createTransactionRequest.Currency,
                TransactionId = response.Content.Id
            };

            var captureResponse = transactionService.CaptureTransaction(captureRequest);

            var refundRequest = new RefundTransactionRequest()
            {
                TransactionId = captureResponse.Content.Id,
                Amount = captureResponse.Content.Amount - 1
            };

            var refundResponse = transactionService.RefundTransaction(refundRequest);

            var transaction = refundResponse.Content;

            Assert.IsFalse(refundResponse.IsError);
            Assert.AreEqual(201, refundResponse.ResponseCode);
            Assert.IsNotNull(transaction.Id);
            Assert.AreEqual(captureResponse.Content.Amount - 1, transaction.RefundedAmount);
            Assert.AreEqual(createTransactionRequest.Amount, transaction.CapturedAmount);
        }

        [TestMethod]
        public void VoidTransaction_Success()
        {
            var transactionId = transactionService.CreateTransaction(createTransactionRequest).Content.Id;

            CaptureTransactionRequest captureRequest = new CaptureTransactionRequest()
            {
                Amount = createTransactionRequest.Amount,
                Currency = createTransactionRequest.Currency,
                TransactionId = transactionId
            };

            var voidRequest = new VoidTransactionRequest()
            {
                TransactionId = transactionId,
                Amount = createTransactionRequest.Amount
            };

            var voidResponse = transactionService.VoidTransaction(voidRequest);

            var transaction = voidResponse.Content;

            Assert.IsFalse(voidResponse.IsError);
            Assert.AreEqual(201, voidResponse.ResponseCode);
            Assert.IsNotNull(transaction.Id);
            Assert.AreEqual(createTransactionRequest.Amount, transaction.VoidedAmount);
        }
    }
}
