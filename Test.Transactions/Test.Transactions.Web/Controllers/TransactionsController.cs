using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Transactions.Common;
using Test.Transactions.Core;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService service)
        {
            _transactionsService = service;
        }

        [HttpGet]
        public ActionResult ViewTransactions(int? pagenumber, int? pageSize)
        {
            if (pagenumber == null) pagenumber = 1;
            if (pageSize == null) pageSize = 10;
            var transactions = _transactionsService.GetTransactions();
            return View("Transactions", new TransactionsViewModel(transactions, pagenumber.Value, pageSize.Value));
        }

        public ActionResult EditTransaction(int? transactionId)
        {
            if(transactionId == null) ModelState.AddModelError("InvalidRequest","Invalid transaction id");
            var transaction = _transactionsService.GetTransaction(transactionId.Value);
            return View("View", new TransactionModel()
                {
                    Id = transaction.Id.Value,
                    Account = transaction.Account,
                    Amount = transaction.Amount == null ? 0.0M : transaction.Amount.Value,
                    CurrencyCode = transaction.CurrencyCode,
                    Description = transaction.Description
                });
        }

        [HttpPost]
        public ActionResult SaveTransaction(TransactionModel transaction)
        {
            if (transaction == null) ModelState.AddModelError("ValidationError", "Invalid Transaction data");
            if (!ModelState.IsValid) return View("View", transaction);
 
            var dto = new TransactionDto(transaction.Id,transaction.Account, transaction.Description, transaction.CurrencyCode,
                                         transaction.Amount);
            if (!ModelState.IsValid) return View("View", transaction);

            var tran = new Transaction(dto);
            if (!tran.IsValid()) ModelState.AddModelError("ValidationError","Invalid Transaction data");

            _transactionsService.SaveTransaction(tran);
            ViewBag.SuccessMessage = "Transaction saved successfully";
            return View("View", transaction);
        }

        public ActionResult DeleteTransaction(int? transactionId)
        {
            if (transactionId == null) ModelState.AddModelError("InvalidRequest", "Invalid transaction id");
            var transaction = _transactionsService.GetTransaction(transactionId.Value);
            if (transaction == null) ModelState.AddModelError("InvalidRequest", "Invalid transaction id");
            if (!ModelState.IsValid) return RedirectToAction("ViewTransactions", "Transactions");
            return View("Delete", new TransactionModel()
            {
                Id = transaction.Id.Value,
                Account = transaction.Account,
                Amount = transaction.Amount == null ? 0.0M : transaction.Amount.Value,
                CurrencyCode = transaction.CurrencyCode,
                Description = transaction.Description
            });
        }
        
        [HttpPost]
        public ActionResult DeleteTransaction(TransactionModel transaction)
        {
            if (transaction == null)
            {
                ModelState.AddModelError("InvalidRequest", "Invalid transaction");
            }
            if(ModelState.IsValid) _transactionsService.DeleteTransaction(transaction.Id);
            return RedirectToAction("ViewTransactions", "Transactions");
        }

        [HttpPost]
        public ActionResult DeleteAllTransactions()
        {
            _transactionsService.DeleteAllTransactions();
            return RedirectToAction("Index", "Home");
        }
    }
}