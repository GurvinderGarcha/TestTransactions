using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Test.Transactions.Common;

namespace Test.Transactions.Core
{
    public interface IUploadService
    {
        void ParseFile(string filename);
    }

    public class UploadService : IUploadService
    {
        private ITransactionsService _service;

        public UploadService(ITransactionsService service)
        {
            _service = service;
        }

        public void ParseFile(string filename)
        {
            var transactions = ParseFileForTransaction(filename);
            foreach (var transaction in transactions)
            {
                _service.SaveTransaction(transaction);
            }
            //parser.ParseFile(filename);

        }

        private List<Transaction> ParseFileForTransaction(string filename)
        {
            var fileContent = File.ReadAllLines(filename);
            var linesIgnored = new List<string>();
            var transactions = new List<Transaction>();
            var headerRow = fileContent.FirstOrDefault();
            if (string.IsNullOrEmpty(headerRow)) throw new Exception("Invalid Header Row");
            var cols = headerRow.Split(',');
            var accountCol = cols.ToList().IndexOf("Account");
            var descriptionCol = cols.ToList().IndexOf("Description");
            var currencyCodeCol = cols.ToList().IndexOf("Currency Code");
            var amountCol = cols.ToList().IndexOf("Amount");
            if (accountCol == -1 || descriptionCol == -1 || currencyCodeCol == -1 || amountCol == -1) throw new Exception("Invalid column headers");

            foreach (string csvRow in fileContent.Skip(1))
            {
                if (string.IsNullOrEmpty(csvRow)) continue;
                try
                {
                    var values = csvRow.Split(',');
                    transactions.Add(new Transaction(new TransactionDto(null, values[accountCol], values[descriptionCol], values[currencyCodeCol], decimal.Parse(values[amountCol]))));
                }
                catch
                {
                    linesIgnored.Add(csvRow);
                }
            }

            return transactions;
        }
    }

    public enum ParserType
    {
        ExcelParser,
        CsvParser
    }

    //public class FileParserFactory
    //{
    //    public static IFileParser GetParser(ParserType parser)
    //    {
    //        switch (parser)
    //        {
    //            case ParserType.CsvParser: return new FileParser();
    //            default: return new FileParser();
    //        }
    //    }
    //}

    //public interface IFileParser
    //{
    //    void ParseFile(string filename);
    //}

    //public class FileParser : IFileParser
    //{
        
    //}
}