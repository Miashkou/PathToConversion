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

        public static string GetPathReferrer(List<Transaction> path)
        {
            if (path.Any(
                a =>
                    a.TransactionType == TransactionValues.Impression ||
                    a.TransactionType == TransactionValues.Click)) return "Campaign";
            return GetReferrerType(GetFirsLogPoint(path));
        }

        // Change to get last session first log point
        public static Transaction GetFirsLogPoint(List<Transaction> path)
        {
            return path.FirstOrDefault(trans => trans.TransactionType == TransactionValues.TrackingPoint);
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

        // Change this to use session attribution
        public static string GetAdInteractionStr(Transaction attributedToTrans)
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

        public static string GetAggregatedPath(List<Transaction> path)
        {
            var fullPath = AggregateMedia(path);
            return string.Join(" -> ", fullPath); // → no unicode in console ;(
        }

        // Fix two leads paths
        private static List<string> AggregateMedia(List<Transaction> path)
        {
            if (path.Count == 1) return new List<string> { path[0].Media };

            var result = new List<string>();

            var lastMedia = path[0].Media;
            var mediaCount = 1;
            foreach (var trans in path.Skip(1))
            {
                var newMedia = trans.Media;
                if (newMedia == lastMedia)
                {
                    mediaCount++;
                    continue;
                }
                result.Add(mediaCount > 1 ? $"[{lastMedia} x{mediaCount}]" : $"[{lastMedia}]");
                lastMedia = newMedia;
                mediaCount = 1;
            }
            return result;
        }
    }
}
