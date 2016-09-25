﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Colorful;
using Microsoft.Win32;
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
            var orderedTransactions = transactionList.OrderBy(r => r.LogTime).ToList();
            foreach (var transactions in orderedTransactions)
            {
                if (!successCookie.Equals(transactions.CookieId)) continue;
                Transactions attribute = null;
                foreach (var transactionInAtribution in Attribution.GetAtribution(transactionList))
                {
                     attribute = !IsNullOrEmpty(transactions.Campaign) ? transactions : transactionInAtribution;
                }
                Console.WriteLine(
                    attribute != null
                        ? $"{transactions.LogTime,-19}|{transactions.TransactionType,-9}|{attribute.Campaign,-16}|{attribute.Media,-10}|{attribute.Banner,-17}|{transactions.ID_LogPoints,-12}|{transactions.URLfrom,-20}"
                        : $"{transactions.LogTime,-19}|{transactions.TransactionType,-9}|{"Empty value",-16}|{"Empty value",-10}|{"Empty value",-17}|{transactions.ID_LogPoints,-12}|{transactions.URLfrom,-20}",
                    Color.White);
                if (!TransactionValues.ClientThankYouLogPoint.Contains(transactions.ID_LogPoints)) continue;
                Console.WriteLine("Edvardo metodai");
                PathPrinter(successCookie,orderedTransactions);
                break;
            }
            Console.WriteLine();
        }
        public static void PathPrinter(int cookieId, List<Transactions> transactionList)
        {
            //Console.WriteLine($"Completed conversion from cookieUser {cookieId}.", Color.SpringGreen);
            //Program.Main()
            //Console.WriteLine($"[Lead | {GetAdInteraction(transactionList)} | {GetReferrers(transactionList)}]", Color.CornflowerBlue);
            //Console.WriteLine();
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


