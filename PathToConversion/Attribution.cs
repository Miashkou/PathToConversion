using System;
using System.Collections.Generic;
using System.Linq;

namespace PathToConversion
{
    internal class Attribution
    {
        public static Transaction GetAttribution(List<Transaction> listWithTransaction, Transaction logPoint)
        {
            // Rename variables (today -> logpointTime)
            // Use passed transaction LogTime instead
            var logpointTime = logPoint.LogTime; //Sessions.GetFirsLogPoint(listWithTransaction).LogTime;
            var sevenDaysEarlier = logpointTime.AddDays(-7);
            var twentyEightDaysEarlier = logpointTime.AddDays(-28);

            var orderedTransactions = listWithTransaction.OrderByDescending(r => r.LogTime).ToList();
            // Get index of logPoint transaction
            var indexOflogPoint = orderedTransactions.IndexOf(logPoint);
            Transaction lastImpression = null;
            // Make a for loop from logPoint index to beginning
            for (int i = indexOflogPoint; i >= 0; i--)
            {
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
            }

            return lastImpression;
        }
    }
}
