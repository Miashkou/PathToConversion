using System;

namespace PathToConversion
{
    internal class Program
    {
        public static void Main()
        { 
            //Enter one of json files to getFileInformation:
            //1)adInteractionTransactions
            //2)deadCookieTransactions
            //3)defaultTransactions
            //4)doubleTransactions
            //5)FailuteTransactions
            //6)mediaPathTransactions
            //7)referringTransactions
            //8)successTransactions
            //9)testClient
            //10)transactions
            //11)bigdata
            Filter.GetSuccessfulTransaction(DataReader.GetFileInformation("transactions"));
            Console.Read();
        }
    }
}
