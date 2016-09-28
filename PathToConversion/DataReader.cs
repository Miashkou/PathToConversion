using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace PathToConversion
{
    internal class DataReader
    {
        public static List<Transactions> GetFileInformation(string fileName)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var json = File.ReadAllText(assemblyPath + "\\" + fileName + ".txt");
            return JsonConvert.DeserializeObject<List<Transactions>>(json);
        }
    }
}
