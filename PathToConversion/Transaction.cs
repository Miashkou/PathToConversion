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
    }
}
