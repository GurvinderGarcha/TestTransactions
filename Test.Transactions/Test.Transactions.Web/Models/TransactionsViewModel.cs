using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using Test.Transactions.Common;
using Test.Transactions.Core;

namespace WebApplication2.Models
{
    public class TransactionsViewModel
    {
        public IEnumerable<Transaction> Transactions { get; private set; }

        public PagedList<Transaction> PagedTransactions { get; private set; }

        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

        public TransactionsViewModel(IEnumerable<Transaction> transactions, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Transactions = transactions;
            PagedTransactions = new PagedList<Transaction>(transactions, pageNumber,pageSize);
        }
    }
}