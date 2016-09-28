using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables.Core;
using static System.ConsoleColor;
using static System.String;


namespace PathToConversion
{
    public static class Filter
    {
        public static void GetSuccessfulTransaction(List<Transactions> transactionList)
        {
            var idLogPointsTemp = "null";
            foreach (var successTransaction in transactionList)
            {
                idLogPointsTemp = !IsNullOrEmpty(successTransaction.ID_LogPoints) ? successTransaction.ID_LogPoints : idLogPointsTemp;
                if (!TransactionValues.ClientThankYouLogPoint.Contains(idLogPointsTemp)) continue;
                idLogPointsTemp = "null";
                CreateTransactionPath(successTransaction.CookieId, transactionList.Where(r => r.CookieId == successTransaction.CookieId).ToList());
            }
        }

        public static void CreateTransactionPath(int successCookie, List<Transactions> transactionList)
        {
            var printTransactions = new ConsoleTable("Logtime", "TransactionType", "Campaign", "Media", "Banner", "ID_LogPoints", "URLfrom");

            var orderedTransactions = transactionList.OrderBy(r => r.LogTime).ToList();
            foreach (var transaction in orderedTransactions)
            {
                if (!successCookie.Equals(transaction.CookieId)) continue;
                var attribute = !IsNullOrEmpty(transaction.Campaign) || !IsNullOrEmpty(transaction.Media) || !IsNullOrEmpty(transaction.Banner) ? transaction : Attribution.GetAtribution(transactionList);

                if (attribute != null)
                    printTransactions.AddRow(transaction.LogTime, transaction.TransactionType, attribute.Campaign, attribute.Media, attribute.Banner, transaction.ID_LogPoints, transaction.URLfrom);
                else
                    printTransactions.AddRow(transaction.LogTime, transaction.TransactionType, Empty, Empty, Empty, transaction.ID_LogPoints, transaction.URLfrom);

                if (!TransactionValues.ClientThankYouLogPoint.Contains(transaction.ID_LogPoints)) continue;
                Console.ForegroundColor = Green;
                Console.WriteLine("******************************************************************************************************************************************");

                TransactionSessionPrinter(successCookie, orderedTransactions);
                break;
            }
            Console.ForegroundColor = White;
            printTransactions.Write();
        }

        public static void TransactionSessionPrinter(int cookieId, List<Transactions> transactionList)
        {
            Console.WriteLine($"Completed conversion from cookieUser {cookieId}.");

            Console.WriteLine(Sessions.GetAggregatedPath(transactionList) + " -> " +
                $"[Lead | {Sessions.GetAdInteractionStr(Attribution.GetAtribution(transactionList))} | {Sessions.GetPathReferrer(transactionList)}]");

            Console.WriteLine();
        }
    }

    public class TransactionValues
    {
        public static int Impression { get; } = 1;
        public static int Click { get; } = 2;
        public static int Event { get; } = 21;
        public static int Unload { get; } = 4;
        public static int TrackingPoint { get; } = 100;

        public static List<string> ClientThankYouLogPoint = new List<string>
        {
            "3240",
            "1001"
        };
    }

}


