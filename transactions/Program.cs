using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace transactions
{
    class Program
    {
        static void Main(string[] args)
        { 
            Atribution.ReadingJson();
            Atribution.FindLeads();
          //  Console.ReadLine();
        }
    }
}
