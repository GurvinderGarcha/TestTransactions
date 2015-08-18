using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Test.Transactions.Common;

namespace Test.Transactions.Core
{
    public class CsvParserService : IParserService
    {
        private readonly ITransactionsService _service;
        private readonly ICurrencyService _currencyService;

        public CsvParserService(ITransactionsService service, ICurrencyService currencyService)
        {
            _service = service;
            _currencyService = currencyService;
        }

        public ParseResults ParseFile(string filename)
        {
            var linesIgnored = new List<string>();
            var transactions = new List<Transaction>();
            try
            {
                var fileContent = File.ReadAllLines(filename);
                var headerRow = fileContent.FirstOrDefault();
                if (string.IsNullOrEmpty(headerRow)) throw new Exception("Invalid Header Row");
                var cols = headerRow.Split(',');
                var accountCol = cols.ToList().IndexOf("Account");
                var descriptionCol = cols.ToList().IndexOf("Description");
                var currencyCodeCol = cols.ToList().IndexOf("Currency Code");
                var amountCol = cols.ToList().IndexOf("Amount");
                if (accountCol == -1 || descriptionCol == -1 || currencyCodeCol == -1 || amountCol == -1)
                    throw new Exception("Invalid column headers");

                foreach (var csvRow in fileContent.Skip(1).Where(csvRow => !string.IsNullOrEmpty(csvRow)))
                {
                    try
                    {
                        var values = csvRow.Split(',');
                        var tran = new Transaction(new TransactionDto(null, values[accountCol], values[descriptionCol],values[currencyCodeCol], decimal.Parse(values[amountCol])));
                        if(tran.IsValid() && _currencyService.IsValidCurrency(tran.CurrencyCode))transactions.Add(tran);
                        else linesIgnored.Add(csvRow);
                    }
                    catch
                    {
                        linesIgnored.Add(csvRow);
                    }
                }

                foreach (var transaction in transactions)
                {
                    _service.SaveTransaction(transaction);
                }
                
            }
            catch (Exception ex)
            {
                return ParseResults.Error(ex.Message);
            }

            return linesIgnored.Any() ? ParseResults.Warning(transactions.Count, linesIgnored): ParseResults.Ok(transactions.Count);
        }
    }
}