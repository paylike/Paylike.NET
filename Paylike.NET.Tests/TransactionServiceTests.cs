using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paylike.NET.Constants;
using Paylike.NET.Interfaces;
using Paylike.NET.RequestModels.Transactions;
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
