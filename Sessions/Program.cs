using System;
using System.Collections.Generic;
using System.Linq;
using PathToConversion;

namespace Sessions
{
    // Changed Transactions from internal to public

    // Rename "Transactions.cs" to "Transaction.cs"?
            
    // Change Transactions.cs to have appropriate types instead of string everywhere
    class Program
    {
        static void Main()
        {

        }

        public static List<string> SearchEngines = new List<string> { "google" };
        public static List<string> SocialMediaSites = new List<string> { "facebook" };
        public static List<string> ReferringSites = new List<string> { "orai.lt" };

        public static readonly TimeSpan RecentAdInteractionSpan = TimeSpan.FromSeconds(30);

        public const int Impression = 1;
        public const int Click = 2;
        public const int Unload = 4;
        public const int Event = 21;
        public const int TrackingPoint = 100;

        static string GetPathReferrer(List<Transactions> path)
        {
            return GetReferrerType(GetFirsLogPoint(path));
        }

        private static Transactions GetFirsLogPoint(List<Transactions> path)
        {
            foreach (var trans in path)
            {
                if (int.Parse(trans.TransactionType) == TrackingPoint)
                    return trans;
            }
            return null;
        }

        private static string GetReferrerType(Transactions trans)
        {
            var url = trans.URLfrom;
            if (url == null) return "Direct link";
            if (SearchEngines.Any(s => url.Contains(s))) return "Natural search";
            if (SocialMediaSites.Any(s => url.Contains(s))) return "Social media";
            if (ReferringSites.Any(s => url.Contains(s))) return "Referring site";
            return "Referrer unknown";
        }

        static bool GetRecentAdInteraction(List<Transactions> path)
        {
            var logPointTime = DateTime.Parse(GetFirsLogPoint(path).LogTime);
            return path.Any(a => logPointTime - DateTime.Parse(a.LogTime) < RecentAdInteractionSpan);
        }

        static string GetAdInteractionStr(Transactions attributedToTrans)
        {
            switch (int.Parse(attributedToTrans.TransactionType))
            {
                case 1:
                    return "Post-Impression";
                case 2:
                    return "Post-Click";
                default:
                    return "";
            }
        }
    }
}
