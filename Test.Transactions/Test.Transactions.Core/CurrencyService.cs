using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Test.Transactions.Core
{
    public class CurrencyService : ICurrencyService
    {
        private readonly List<string> _currencies;
 
        public CurrencyService()
        {
            _currencies = new List<string>();
            LoadCurrencies();
        }

        private void LoadCurrencies()
        {
            foreach (var regionInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(cultureInfo => new RegionInfo(cultureInfo.LCID)).Where(regionInfo => !_currencies.Contains(regionInfo.ISOCurrencySymbol)))
            {
                _currencies.Add(regionInfo.ISOCurrencySymbol);
            }
        }

        public bool IsValidCurrency(string currencyCode)
        {
            return _currencies.Contains(currencyCode);
        }
    }
}