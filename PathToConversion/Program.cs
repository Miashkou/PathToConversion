using System;

namespace PathToConversion
{
    internal class Program
    {
        public static void Main()
        { 
            Filter.GetSuccessfulTransaction(DataReader.GetTracks);
            Console.Read();
        }
    }
}
