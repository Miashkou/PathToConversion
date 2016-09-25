using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace PathToConversion
{
    internal class DataReader
    {
        private static readonly string AssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string Json = File.ReadAllText(AssemblyPath + "\\transactions.txt");
       public static List<Transactions> GetTracks = JsonConvert.DeserializeObject<List<Transactions>>(Json);
    }
}
