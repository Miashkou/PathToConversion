using System;

namespace PathToConversion
{
    internal class Program
    {
        public static void Main()
        { 
            Filter.GetSuccessfulTransaction(DataReader.GetFileInformation("bigdata"));
            Console.Read();
        }
    }
}
