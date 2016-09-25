using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace transactions
{
    public static class Atribution
    {
        private static List<Transactions> DeserializeObject = new List<Transactions>();
   
        public static void ReadingJson()
        {
            using (StreamReader r = new StreamReader("transactions.json"))
            {
                var json = r.ReadToEnd();
                DeserializeObject = JsonConvert.DeserializeObject<List<Transactions>>(json);
                Console.WriteLine("{0,-19}|{1,-15}|{2,-15}|{3,-10}|{4,-18}|{5,-15}|{6,-15}|", "LogTime",
                    "Atribution", "Campaign", "Media", "Banner", "ID_LogPoints", "CookieId");
            }
        }

        public static void FindLeads()
        {
            var nullString = "null";
            foreach (var tran in DeserializeObject)
            {

                nullString = !string.IsNullOrEmpty(tran.ID_LogPoints) ? tran.ID_LogPoints : nullString;
                if ("3240".Contains(nullString))
                {
                    nullString = "null";
                    ConversionMethod(tran.CookieID, DeserializeObject.Where(r => r.CookieID == tran.CookieID).ToList());
                }
            }


            foreach (var succesfull in DeserializeObject)
            {
                if (succesfull.TrackingSetup.Contains("Thank"))
                {
                    Console.WriteLine(succesfull.TrackingSetup);
                }
            }


        }

        public static void ConversionMethod(string successCookie, List<Transactions> DeserializeObject)
        { 
            foreach (var transactions in DeserializeObject)
            {
            
                foreach (var transactionInAtribution in GetAtribution(DeserializeObject))
                {
                    if (!transactions.CookieID.Equals(successCookie)) continue;
                    var atribution = !string.IsNullOrEmpty(transactions.Campaign)
                        ? transactions
                        : transactionInAtribution;
                   
                    Console.WriteLine("{0,-10}|{1,-15}|{2,-15}|{3,-10}|{4,-18}|{5,-15}|{6,-15}|", transactions.LogTime, transactions.TransactionType, atribution.Campaign, 
                        atribution.Media, atribution.Banner, transactions.ID_LogPoints, transactions.CookieID);
                }     
            }
            Console.ReadLine();
        }
       

        public static List<Transactions> GetAtribution(List<Transactions> listWithTransaction)
        {
            var orderedTransactions = listWithTransaction.OrderByDescending(r => r.LogTime).ToList();

            List<Transactions> transactionList = new List<Transactions>();
            foreach (var transaction in orderedTransactions)
            {
                if (transaction.TransactionType == TransactionTypes.Click)
                {
                    List<Transactions> transactionClick = new List<Transactions>();
                    transactionClick.Add(transaction);

                    return transactionClick;
            
                }

                else if ((transaction.TransactionType == TransactionTypes.Impression))
                {
                    transactionList.Add(transaction);  
                }

            }
            return transactionList;
        }


    }
}
