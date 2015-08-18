using System.Collections.Generic;

namespace Test.Transactions.Common
{
    public enum ParseStatus
    {
        Ok,
        Warning,
        Failed
    }

    public class ParseResults
    {
        public int RecordCount { get;private set; }

        public List<string> InvalidLines { get; private set; }

        public string Message { get; private set; }

        public ParseStatus Status { get; private set; }

        public static ParseResults Ok(int recordCount)
        {
            return new ParseResults() {RecordCount = recordCount, InvalidLines = new List<string>(), Status = ParseStatus.Ok};
        }

        public static ParseResults Warning(int recordCount, List<string> invalidLines)
        {
            return new ParseResults() { RecordCount = recordCount, InvalidLines = invalidLines, Status = ParseStatus.Warning };
        }

        public static ParseResults Error(string message)
        {
            return new ParseResults() { Message = message ,Status = ParseStatus.Failed };
        }
    }
}