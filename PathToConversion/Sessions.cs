using System;
using System.Collections.Generic;
using System.Linq;

namespace PathToConversion
{
    class Sessions
    {
        public static List<string> SearchEngines = new List<string> {
            "google",
            "yahoo",
            "bingo"};

        public static List<string> SocialMediaSites = new List<string> {
            "facebook",
            "linkedin",
            "twiter",
            "alfa" };

        public static List<string> ReferringSites = new List<string> { "orai.lt" };

        public static readonly TimeSpan RecentAdInteractionSpan = TimeSpan.FromSeconds(30);

        public static string GetPathReferrer(List<Transactions> path)
        {
            if (
                path.Any(
                    a =>
                        a.TransactionType == TransactionValues.Impression ||
                        a.TransactionType == TransactionValues.Click))
                return "Campaign";
            return GetReferrerType(GetFirsLogPoint(path));
        }

        private static Transactions GetFirsLogPoint(List<Transactions> path)
        {
            foreach (var trans in path)
            {
                if (trans.TransactionType == TransactionValues.TrackingPoint)
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
            return "Referring site";
        }

        public static bool GetRecentAdInteraction(List<Transactions> path)
        {
            var logPointTime = GetFirsLogPoint(path).LogTime;
            return path.Any(a => logPointTime - a.LogTime < RecentAdInteractionSpan);
        }

        public static string GetAdInteractionStr(Transactions attributedToTrans)
        {
            if (attributedToTrans == null) return "Non-Campaign";
            switch (attributedToTrans.TransactionType)
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
