using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transactions
{
    public class Transactions
    {
        public string LogTime { get; set; }
        public int TransactionType { get; set; }
        public string CookieID { get; set; }
        public int CookiesEnabled { get; set; }
        public string Campaign { get; set; }
        public string Media { get; set; }
        public string Banner { get; set; }
        public string TrackingSetup { get; set; }

        public string ID_LogPoints { get; set; }

        public string LogPointName { get; set; }
        public string URLfrom { get; set; }
        public string URLto { get; set; }
        public string ClientSite { get; set; }

    }
}
