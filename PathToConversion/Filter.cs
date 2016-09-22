using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Colorful;
using static System.String;

namespace PathToConversion
{
    internal class Filter
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

        public static void CreateTransactionPath(string successCid, List<Transactions> transactionList)
        {
            var cookieId = successCid;
            var orderedTransactions = transactionList.OrderBy(r => r.LogTime).ToList();
            var reversedTransactions = transactionList.OrderByDescending(r => r.LogTime).ToList();
            foreach (var tracked in orderedTransactions)
            {
                if (!cookieId.Equals(tracked.CookieId)) continue;
                var logTime = !IsNullOrEmpty(tracked.LogTime) ? tracked.LogTime : "null";
                var transactionType = !IsNullOrEmpty(tracked.TransactionType) ? tracked.TransactionType : "null";
                //TODO Eivino metodai to get filled transactions
                //var campaign = !IsNullOrEmpty(tracked.Campaign) ? tracked.Campaign : Method.GetLastCampaign(reversedTransactions);
                //var media = !IsNullOrEmpty(tracked.Media) ? tracked.Media : Method.GetLastMedia(reversedTransactions);
                //var banner = !IsNullOrEmpty(tracked.Banner) ? tracked.Banner : Method.GetLastBanner(reversedTransactions);
                var idLogPoints = !IsNullOrEmpty(tracked.ID_LogPoints) ? tracked.ID_LogPoints : "null";
                // Console.WriteLine($"{logTime.PadRight(19)}|{transactionType.PadRight(9)}|{campaign.PadRight(16)}|{media.PadRight(10)}|{banner.PadRight(17)}|{idLogPoints.PadRight(5)}|", Color.White);
                if (!TransactionValues.ClientThankYouLogPoint.Contains(idLogPoints)) continue;
                //TODO Edvardo metodai spausdinti referrerius ir kt.
                break;
            }
        }
    }

    public class TransactionValues
    {
        public static string Impression { get; } = "1";
        public static string Click { get; } = "2";
        public static string Event { get; } = "21";
        public static string Unload { get; } = "4";
        public static string TrackingPoint { get; } = "100";

        public static List<string> ClientThankYouLogPoint = new List<string>
        {
            "3240",
            "1001"
        };
    }

}


