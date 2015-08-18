using Test.Transactions.Common;

namespace Test.Transactions.Core
{
    public interface IParserService
    {
        ParseResults ParseFile(string filename);
    }
}