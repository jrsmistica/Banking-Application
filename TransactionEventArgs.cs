using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment_4
{
    internal class TransactionEventArgs
    {
        public class TransactionEventArgs : LoginEventArgs
        {
            public decimal Amount { get; }

            public TransactionEventArgs(string personName, decimal amount, bool success)
                : base(personName, success)
            {
                Amount = amount;
            }
        }

    }
}
