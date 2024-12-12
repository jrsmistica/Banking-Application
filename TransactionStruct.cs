using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    internal class TransactionStruct
    {
        public struct Transaction
        {
            public string AccountNumber { get; }
            public decimal Amount { get; }
            public Person Originator { get; }
            public DayTime Time { get; }

            public Transaction(string accountNumber, decimal amount, Person originator, DayTime time)
            {
                AccountNumber = accountNumber;
                Amount = amount;
                Originator = originator;
                Time = time;
            }

            public override string ToString()
            {
                string operation = Amount >= 0 ? "Deposit" : "Withdraw";
                return $"{AccountNumber} {operation} by {Originator.Name} of {Math.Abs(Amount):C} on {Time}";
            }
        }

    }
}
