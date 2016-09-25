using System.Collections.Generic;
using System.Linq;

namespace PathToConversion
{
    internal class Attribution
    {
        public static List<Transactions> GetAtribution(List<Transactions> listWithTransaction)
        {
            var orderedTransactions = listWithTransaction.OrderByDescending(r => r.LogTime).ToList();
            var transactionList = new List<Transactions>();
            foreach (var transaction in orderedTransactions)
            {
                if (transaction.TransactionType.Equals(TransactionValues.Click))
                {
                    var transactionClick = new List<Transactions> { transaction };
                    return transactionClick;
                }
                else if ((transaction.TransactionType.Equals(TransactionValues.Impression)))
                {
                    transactionList.Add(transaction);
                }
            }
            return transactionList;
        }
    }
}
