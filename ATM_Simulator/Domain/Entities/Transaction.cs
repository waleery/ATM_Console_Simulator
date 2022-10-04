using System;
using ATM_Simulator.Domain.Enum;

namespace ATM_Simulator.Domain.Entities
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public long UserBankAccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType  { get; set; }
        public string Description { get; set; }
        public Decimal TransactionAmount { get; set; }
    }
}

