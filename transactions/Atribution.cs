using System;
using System.Collections.Generic;
using System.Linq;
using PathToConversion;


namespace transactions
{
    public static class Atribution
    {

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
