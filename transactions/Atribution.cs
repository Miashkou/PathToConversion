using System;
using System.Collections.Generic;
using System.Linq;
using PathToConversion;

namespace transactions
{
    class Atribution
    {

        public static void ConversionMethod(string successCookie, List<Transactions> deserializeObject)
        {
            foreach (var transactions in deserializeObject)
            {

                foreach (var transactionInAtribution in GetAtribution(deserializeObject))
                {
                    if (!transactions.CookieID.Equals(successCookie)) continue;
                    var atribution = !string.IsNullOrEmpty(transactions.Campaign) ? transactions : transactionInAtribution;

                    Console.WriteLine("{0,-10}|{1,-15}|{2,-15}|{3,-10}|{4,-18}|{5,-15}|{6,-15}|",
                        transactions.LogTime, transactions.TransactionType, atribution.Campaign,
                        atribution.Media, atribution.Banner, transactions.ID_LogPoints, transactions.CookieID);
                }
            }
            Console.ReadLine();
        }


        public static List<Transactions> GetAtribution(List<Transactions> listWithTransaction)
        {
            var orderedTransactions = listWithTransaction.OrderByDescending(r => r.LogTime).ToList();
            var transactionList = new List<Transactions>();
            foreach (var transaction in orderedTransactions)
            {
                if (transaction.TransactionType == TransactionValues.Click)
                {
                    var transactionClick = new List<Transactions> { transaction };
                    return transactionClick;
                }
                else if ((transaction.TransactionType == TransactionValues.Impression))
                {
                    transactionList.Add(transaction);
                }
            }
            return transactionList;
        }
    }
}
