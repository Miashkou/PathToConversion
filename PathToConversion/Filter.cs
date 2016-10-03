using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables.Core;
using static System.ConsoleColor;
using static System.String;

namespace PathToConversion
{
    public class Filter
    {
        public static void GetSuccessfulTransaction(List<Transaction> transactionList)
        {
            var orderedTransactions = transactionList.OrderBy(r => r.LogTime).ToList();
            foreach (var client in orderedTransactions.Select(transaction => transaction.ClientSite).Distinct().ToList())
            {
                foreach (var transaction in orderedTransactions.Where(r => r.ClientSite == client))
                {
                    if (!(TransactionValues.ClientThankYouLogPoint.Contains(transaction.ID_LogPoints) && TransactionValues.TrackingPoint == transaction.TransactionType)) continue;
                    CreateTransactionPath(transaction.CookieId, transactionList.Where(r => r.CookieId == transaction.CookieId).ToList(), transaction.LogTime, client);
                }
            }

        }

        public static void CreateTransactionPath(int successCookieId, List<Transaction> transactionList, DateTime timer, string client)
        {
            var printTransactions = new ConsoleTable("Logtime", "TransactionType", "Campaign", "Media", "Banner", "ID_LogPoints", "URLfrom");
            var orderedTransactions = transactionList.Where(r => r.ClientSite == client).OrderBy(r => r.LogTime).ToList();
            var transactionsByLogTime = new List<Transaction>();
            foreach (var transaction in orderedTransactions)
            {
                if (transaction.LogTime > timer) continue;
                var attribute = transaction.TransactionType != TransactionValues.TrackingPoint
                    ? transaction
                    : Attribution.GetAttribution(orderedTransactions, transaction, timer);
               
                transactionsByLogTime.Add(transaction);
                if (attribute != null)
                    printTransactions.AddRow(transaction.LogTime, transaction.TransactionType, attribute.Campaign,
                        attribute.Media, attribute.Banner, transaction.ID_LogPoints, transaction.URLfrom);
                else
                    printTransactions.AddRow(transaction.LogTime, transaction.TransactionType, Empty, Empty, Empty,
                        transaction.ID_LogPoints, transaction.URLfrom);
            }
            TransactionSessionPrinter(successCookieId, transactionsByLogTime);
            Console.ForegroundColor = Cyan;
            printTransactions.Write();
        }

        public static void TransactionSessionPrinter(int cookieId, List<Transaction> transactionList)
        {
            Console.ForegroundColor = Green;
            Console.WriteLine("******************************************************************************************************************************************");
            Console.WriteLine($"Completed conversion from cookieUser {cookieId}.");

            Console.WriteLine(Join(" -> ", Sessions.GetLastSessionFirstPoint(transactionList),
                $"[Lead | {Sessions.GetAdInteractionStr(transactionList)} | {Sessions.GetPathReferrer(transactionList)}]"));

            Console.WriteLine();
        }
    }

    public class TransactionValues
    {
        public static readonly int Impression = 1;
        public static readonly int Click = 2;
        public static readonly int Event = 21;
        public static readonly int Unload = 4;
        public static readonly int TrackingPoint = 100;

        public static List<string> ClientThankYouLogPoint = new List<string>
        {
            "3240",
            "1001"
        };
    }

}


