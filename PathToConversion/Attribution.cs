using System;
using System.Collections.Generic;
using System.Linq;

namespace PathToConversion
{
    internal class Attribution
    {
        public static Transaction GetAttribution(List<Transaction> listWithTransaction, Transaction logPoint, DateTime logpoinTime)
        {
            var logpointTime = logPoint.LogTime;
            var sevenDaysEarlier = logpointTime.AddDays(-7);
            var twentyEightDaysEarlier = logpointTime.AddDays(-28);

            var orderedTransactions = listWithTransaction.TakeWhile(r => r.LogTime <= logpoinTime).OrderBy(r => r.LogTime).ToList();

            var indexOflogPoint = orderedTransactions.IndexOf(logPoint);
            Transaction lastImpression = null;
            for (int i = indexOflogPoint; i >= 0; i--)
            {
                var transaction = orderedTransactions[i];

                if (transaction.TransactionType.Equals(TransactionValues.Click) && (transaction.LogTime > twentyEightDaysEarlier))
                {
                    return transaction;
                }
                else if ((lastImpression == null) && transaction.TransactionType.Equals(TransactionValues.Impression) && transaction.LogTime > sevenDaysEarlier)
                {

                    lastImpression = transaction;
                }

            }

            return lastImpression;
        }
    }
}
