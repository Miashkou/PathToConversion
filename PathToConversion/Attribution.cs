using System;
using System.Collections.Generic;
using System.Linq;

namespace PathToConversion
{
    internal class Attribution
    {
        public static Transactions GetAtribution(List<Transactions> listWithTransaction)
        {
        var today = Sessions.GetFirsLogPoint(listWithTransaction).LogTime;
        var sevenDaysEarlier = today.AddDays(-7);
        var twentyEightDaysEarlier = today.AddDays(-28);

        var orderedTransactions = listWithTransaction.OrderByDescending(r => r.LogTime).ToList();
            Transactions lastImpression = null;
            foreach (var transaction in orderedTransactions)
            {
                if (transaction.TransactionType.Equals(TransactionValues.Click))
                {
                    if (transaction.LogTime > twentyEightDaysEarlier)
                        return transaction;
                }
                else if (lastImpression == null)
                {
                    if (transaction.TransactionType.Equals(TransactionValues.Impression) && transaction.LogTime > sevenDaysEarlier)
                        lastImpression = transaction;
                }
            }
            return lastImpression;
        }
    }
}
