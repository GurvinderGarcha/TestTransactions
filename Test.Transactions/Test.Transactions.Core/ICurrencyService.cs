using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.Transactions.Core
{
    public interface ICurrencyService
    {
        bool IsValidCurrency(string currencyCode);
    }
}
