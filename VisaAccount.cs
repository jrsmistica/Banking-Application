using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankingConsole
{
    public class VisaAccount : Account, ITransaction
    {
        private const decimal INTEREST_RATE = 0.1995m;
        private decimal creditLimit;

        public VisaAccount(decimal balance = 0, decimal creditLimit = 1200)
            : base("VS-", balance)
        {
            this.creditLimit = creditLimit;
        }

        public void Deposit(decimal amount, Person person)
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

            if (Balance - amount < -creditLimit)
                throw new AccountException(ExceptionType.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);

            base.Deposit(-amount, person);

            OnTransactionOccur(this, new TransactionEventArgs(person.Name, -amount, true));
        }

        public override void PrepareMonthlyReport()
        {
            decimal interest = (LowestBalance * INTEREST_RATE) / 12;

            Balance -= interest;

            transactions.Clear();
        }

        public override string ToString()
        {
            return base.ToString() + $" (Visa Account) - Credit Limit: {creditLimit}";
        }
    }
}
