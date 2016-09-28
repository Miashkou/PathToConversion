using System;

namespace PathToConversion
{
    public class Transaction
    {
        public DateTime LogTime { get; set; }
        public int TransactionType { get; set; }
        public int CookieId { get; set; }
        public string Campaign { get; set; }
        public string Media { get; set; }
        public string Banner { get; set; }
        public string URLfrom { get; set; }
        public string ID_LogPoints { get; set; }
        public string LogPointName { get; set; }

        // Add equals ovveride
        public override bool Equals(object obj)
        {
            return LogTime.Equals(obj);
        }

        protected bool Equals(Transaction other)
        {
            return LogTime.Equals(other.LogTime) && TransactionType == other.TransactionType && CookieId == other.CookieId && string.Equals(Campaign, other.Campaign) && string.Equals(Media, other.Media) && string.Equals(Banner, other.Banner) && string.Equals(URLfrom, other.URLfrom) && string.Equals(ID_LogPoints, other.ID_LogPoints) && string.Equals(LogPointName, other.LogPointName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = LogTime.GetHashCode();
                hashCode = (hashCode*397) ^ TransactionType;
                hashCode = (hashCode*397) ^ CookieId;
                hashCode = (hashCode*397) ^ (Campaign != null ? Campaign.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Media != null ? Media.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Banner != null ? Banner.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (URLfrom != null ? URLfrom.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ID_LogPoints != null ? ID_LogPoints.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LogPointName != null ? LogPointName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
