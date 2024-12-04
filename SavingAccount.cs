﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankingConsole
{
    public class SavingAccount : Account, ITransaction
    {
        private const decimal COST_PER_TRANSACTION = 0.5m;
        private const decimal INTEREST_RATE = 0.015m;

        public SavingAccount(decimal balance = 0)
            : base("SV-", balance)
        {
        }
        public new void Deposit(decimal amount, Person person)
        {
            if (!IsUser(person.Name))
                throw new AccountException(ExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);

            if (!person.IsAuthenticated)
                throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);

            base.Deposit(amount, person);

            OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
        }
        public void Withdraw(decimal amount, Person person)
        {
            if (!IsUser(person.Name))
                throw new AccountException(ExceptionType.NAME_NOT_ASSOCIATED_WITH_ACCOUNT);

            if (!person.IsAuthenticated)
                throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);

            if (Balance < amount)
                throw new AccountException(ExceptionType.NO_OVERDRAFT_FOR_THIS_ACCOUNT);

            Balance -= amount;
            transactions.Add(new Transaction(Number, -amount, person, Utils.Now()));
            OnTransactionOccur(this, new TransactionEventArgs(person.Name, -amount, true));
        }

        public override void PrepareMonthlyReport()
        {
            decimal serviceCharge = transactions.Count * COST_PER_TRANSACTION;

            decimal interest = (LowestBalance * INTEREST_RATE) / 12;

            Balance += interest - serviceCharge;

            transactions.Clear();
        }
    }
}   