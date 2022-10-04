using System;
using ATM_Simulator.Domain.Enum;

namespace ATM_Simulator.Domain.Interfaces
{
    public interface ITransaction
    {
        void InsertTransaction(long _UserBankAccountID, TransactionType _tranType, decimal _tranAmount, string _desc);
        void ViewTransaction();
    }
}

