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

        public static string GetPathReferrer(List<Transactions> path)
        {
            return path.Any(
                a =>
                    a.TransactionType == TransactionValues.Impression ||
                    a.TransactionType == TransactionValues.Click) ? "Campaign" : GetReferrerType(GetFirsLogPoint(path));
        }

        public static Transactions GetFirsLogPoint(List<Transactions> path)
        {
            return path.FirstOrDefault(trans => trans.TransactionType == TransactionValues.TrackingPoint);
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

        public static string GetAggregatedPath(List<Transactions> path)
        {
            var fullPath = AggregateMedia(path);
            return string.Join(" -> ", fullPath); // → no unicode in console ;(
        }

        private static List<string> AggregateMedia(List<Transactions> path)
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
