using System;
using System.Collections.Generic;
using System.Linq;

namespace PathToConversion
{
    internal class Attribution
    {
        private static DateTime _today = DateTime.Today;
        private static readonly DateTime SevenDaysEarlier = _today.AddDays(-7);
        private static readonly DateTime TwentyEightDaysEarlier = _today.AddDays(-28);
        public static List<Transactions> GetAtribution(List<Transactions> listWithTransaction)
        {
            var orderedTransactions = listWithTransaction.OrderByDescending(r => r.LogTime).ToList();
            var transactionList = new List<Transactions>();
            foreach (var transaction in orderedTransactions)
            {
                if (transaction.TransactionType.Equals(TransactionValues.Click))
                {
                    if (transaction.LogTime > TwentyEightDaysEarlier)
                    {
                        var transactionClick = new List<Transactions> {transaction};
                        return transactionClick;
                    }
                }
                else if ((transaction.TransactionType.Equals(TransactionValues.Impression)))
                {
                    if (transaction.LogTime > SevenDaysEarlier)
                        transactionList.Add(transaction);
                }
            }
            return transactionList;
        }
    }
}
