using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignment_4
{
    internal class LoggerClass
    {
        public static class Logger
        {
            private static List<string> loginEvents = new List<string>();
            private static List<string> transactionEvents = new List<string>();

            public static void LoginHandler(object sender, EventArgs args)
            {
                if (args is LoginEventArgs loginArgs)
                {
                    string log = $"{loginArgs.PersonName} login {(loginArgs.Success ? "successful" : "failed")} at {Utils.Now}";
                    loginEvents.Add(log);
                }
            }

            public static void TransactionHandler(object sender, EventArgs args)
            {
                if (args is TransactionEventArgs transArgs)
                {
                    string log = $"{transArgs.PersonName} transaction {transArgs.Amount:C} {(transArgs.Success ? "successful" : "failed")} at {Utils.Now}";
                    transactionEvents.Add(log);
                }
            }

            public static void ShowLoginEvents()
            {
                foreach (var log in loginEvents)
                    Console.WriteLine(log);
            }

            public static void ShowTransactionEvents()
            {
                foreach (var log in transactionEvents)
                    Console.WriteLine(log);
            }
        }

    }
}
