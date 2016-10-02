using System;

namespace PathToConversion
{
    internal class Program
    {
        public static void Main()
        { 
            Filter.GetSuccessfulTransaction(DataReader.GetFileInformation("43Cid"));
            Console.Read();
        }
    }
}
