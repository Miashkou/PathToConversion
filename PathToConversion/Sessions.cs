using System;
using System.Collections.Generic;
using System.Linq;

namespace PathToConversion
{
    internal class Sessions
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

        public static readonly TimeSpan RecentAdInteractionSpan = TimeSpan.FromSeconds(30);

        public static readonly TimeSpan SessionTimeoutSpan = TimeSpan.FromMinutes(30);

        public static string GetPathReferrer(List<Transaction> path)
        {
            if (path.Any(
                a =>
                    a.TransactionType == TransactionValues.Impression ||
                    a.TransactionType == TransactionValues.Click)) return "Campaign";
            return GetReferrerType(GetFirsLogPoint(path));
        }

        public static Transaction GetFirsLogPoint(List<Transaction> path)
        {
            // Assume last is always the lead
            var firstLeadInChain = path.Last();
            foreach (var trans in Enumerable.Reverse(path))
            {
                if (trans.TransactionType != TransactionValues.TrackingPoint) continue;

                if (firstLeadInChain.LogTime - trans.LogTime < SessionTimeoutSpan)
                    firstLeadInChain = trans;
                else return firstLeadInChain;
            }
            return firstLeadInChain;
        }

        private static string GetReferrerType(Transaction trans)
        {
            var url = trans.URLfrom;
            if (url == null) return "Direct link";
            if (SearchEngines.Any(s => url.Contains(s))) return "Natural search";
            if (SocialMediaSites.Any(s => url.Contains(s))) return "Social media";
            return "Referring site";
        }

        public static bool GetRecentAdInteraction(List<Transaction> path)
        {
            var logPointTime = GetFirsLogPoint(path).LogTime;
            return path.Any(a => logPointTime - a.LogTime < RecentAdInteractionSpan);
        }

        public static string GetAdInteractionStr(List<Transaction> transactions, Transaction forTransaction = null)
        {
            if (forTransaction == null)
                forTransaction = transactions.Last();

            var attributedToTrans = Attribution.GetAttribution(transactions, forTransaction, DateTime.Now);

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

        public static string GetLastSessionFirstPoint(List<Transaction> path)
        {
            var fullPath = AggregateMedia(path);
            return string.Join(" -> ", fullPath); // → no unicode in console ;(
        }

        private static List<string> AggregateMedia(List<Transaction> path)
        {
            var result = new List<string>();

            string lastMedia = null;
            var mediaCount = 1;
            foreach (var trans in path)
            {
                if (trans.TransactionType == TransactionValues.TrackingPoint)
                    continue;
                
                var newMedia = trans.Media;

                if (newMedia == lastMedia)
                {
                    mediaCount++;
                }
                else
                {
                    if (lastMedia != null)
                        result.Add(mediaCount > 1 ? $"[{lastMedia} x{mediaCount}]" : $"[{lastMedia}]");
                    lastMedia = newMedia;
                    mediaCount = 1;
                }
            }

            if (lastMedia != null)
                result.Add(mediaCount > 1 ? $"[{lastMedia} x{mediaCount}]" : $"[{lastMedia}]");

            return result;
        }
    }
}
