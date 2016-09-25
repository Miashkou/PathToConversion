using System;
using System.Drawing;
using Console = Colorful.Console;
namespace PathToConversion
{
    internal class Program
    {
        public static void Main()
        {
            Console.Write("Logtime".PadRight(19), Color.Blue);
            Console.Write("|TransType".PadLeft(9), Color.Red);
            Console.Write("|Campaign".PadLeft(9), Color.DarkCyan);
            Console.Write("|Media".PadLeft(14), Color.Coral);
            Console.Write("|Banner".PadLeft(12), Color.Violet);
            Console.Write("|ID_LogPoints".PadLeft(24), Color.Crimson);
            Console.WriteLine("|URLfrom", Color.Firebrick);
            Filter.GetSuccessfulTransaction(DataReader.GetTracks);
            
            Console.Read();
        }
    }
}
