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
        public static void GetSuccessfulTransaction(List<Transaction> transactionList)
        {
            //  var orderedTransactions = transactionList.OrderBy(r => r.LogTime).ToList();
            var idLogPointsTemp = "null";
            foreach (var transaction in transactionList)
            {
                idLogPointsTemp = !IsNullOrEmpty(transaction.ID_LogPoints) ? transaction.ID_LogPoints : idLogPointsTemp;
                if (!TransactionValues.ClientThankYouLogPoint.Contains(idLogPointsTemp)) continue;
                idLogPointsTemp = "null";
                CreateTransactionPath(transaction.CookieId, transactionList.Where(r => r.CookieId == transaction.CookieId).ToList(), transaction.LogTime);
            }
        }

        public static void CreateTransactionPath(int successCookieId, List<Transaction> transactionList, DateTime logtimer)
        {
            var printTransactions = new ConsoleTable("Logtime", "TransactionType", "Campaign", "Media", "Banner", "ID_LogPoints", "URLfrom");
            var orderedTransactions = transactionList.OrderBy(r => r.LogTime).ToList();
            foreach (var transaction in orderedTransactions)
            {
                //TODO atrinkt iki kurio successLogTime spausdinti...
                //while (!logtimer.Equals(transaction.LogTime))
                //{
                    var attribute = transaction.TransactionType != TransactionValues.TrackingPoint ? transaction : Attribution.GetAttribution(transactionList, transaction);

                if (attribute != null)
                    printTransactions.AddRow(transaction.LogTime, transaction.TransactionType, attribute.Campaign,
                        attribute.Media, attribute.Banner, transaction.ID_LogPoints, transaction.URLfrom);
                else
                    printTransactions.AddRow(transaction.LogTime, transaction.TransactionType, Empty, Empty, Empty,
                        transaction.ID_LogPoints, transaction.URLfrom);
                //}
                


            }
            Console.ForegroundColor = Green;
            Console.WriteLine("******************************************************************************************************************************************");

            TransactionSessionPrinter(successCookieId, orderedTransactions);


            Console.ForegroundColor = Cyan;
            printTransactions.Write();
        }

        public static void TransactionSessionPrinter(int cookieId, List<Transaction> transactionList)
        {
            Console.WriteLine($"Completed conversion from cookieUser {cookieId}.");

            Console.WriteLine(Sessions.GetAggregatedPath(transactionList) + " -> " +
                $"[Lead | {Sessions.GetAdInteractionStr(Sessions.GetFirsLogPoint(transactionList))} | {Sessions.GetPathReferrer(transactionList)}]");

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


