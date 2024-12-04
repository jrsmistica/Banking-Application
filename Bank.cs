using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingConsole
{
    public static class Bank
    {
        public static readonly Dictionary<string, Account> ACCOUNTS = new Dictionary<string, Account>();
        public static readonly Dictionary<string, Person> USERS = new Dictionary<string, Person>();

        static Bank()
        {
            // Initialize USERS collection
            AddUser("Narendra", "1234-5678");
            AddUser("Ilia", "2345-6789");
            AddUser("Mehrdad", "3456-7890");
            AddUser("Vinay", "4567-8901");
            AddUser("Arben", "5678-9012");
            AddUser("Patrick", "6789-0123");
            AddUser("Yin", "7890-1234");
            AddUser("Hao", "8901-2345");
            AddUser("Jake", "9012-3456");
            AddUser("Mayy", "1224-5678");
            AddUser("Nicoletta", "2344-6789");

            // Initialize ACCOUNTS collection
            AddAccount(new VisaAccount());                 // VS-100000
            AddAccount(new VisaAccount(150, -500));        // VS-100001
            AddAccount(new SavingAccount(5000));           // SV-100002
            AddAccount(new SavingAccount());               // SV-100003
            AddAccount(new CheckingAccount(2000));         // CK-100004
            AddAccount(new CheckingAccount(1500, true));   // CK-100005
            AddAccount(new VisaAccount(50, -550));         // VS-100006
            AddAccount(new SavingAccount(1000));           // SV-100007

            // Associate users with accounts
            AssociateUserWithAccount("VS-100000", new[] { "Narendra", "Ilia", "Mehrdad" });
            AssociateUserWithAccount("VS-100001", new[] { "Vinay", "Arben", "Patrick" });
            AssociateUserWithAccount("SV-100002", new[] { "Yin", "Hao", "Jake" });
            AssociateUserWithAccount("SV-100003", new[] { "Mayy", "Nicoletta" });
            AssociateUserWithAccount("CK-100004", new[] { "Mehrdad", "Arben", "Yin" });
            AssociateUserWithAccount("CK-100005", new[] { "Jake", "Nicoletta" });
            AssociateUserWithAccount("VS-100006", new[] { "Ilia", "Vinay" });
            AssociateUserWithAccount("SV-100007", new[] { "Patrick", "Hao" });
        }

        // Method to add a user
        public static void AddUser(string name, string sin)
        {
            Person person = new Person(name, sin);
            person.OnLogin += Logger.LoginHandler;
            USERS.Add(name, person);
        }

        // Method to add an account
        public static void AddAccount(Account account)
        {
            account.OnTransaction += Logger.TransactionHandler;
            ACCOUNTS.Add(account.Number, account);
        }

        // Method to associate multiple users with an account
        private static void AssociateUserWithAccount(string accountNumber, string[] userNames)
        {
            if (ACCOUNTS.TryGetValue(accountNumber, out var account))
            {
                foreach (var userName in userNames)
                {
                    if (USERS.TryGetValue(userName, out var user))
                    {
                        account.AddUser(user);
                    }
                }
            }
        }

        public static Account GetAccount(string number)
        {
            if (ACCOUNTS.TryGetValue(number, out var account))
                return account;
            throw new AccountException(ExceptionType.ACCOUNT_DOES_NOT_EXIST);
        }

        // Retrieve a user by name
        public static Person GetUser(string name)
        {
            if (USERS.TryGetValue(name, out var user))
                return user;
            throw new AccountException(ExceptionType.USER_DOES_NOT_EXIST);
        }

        // Save accounts data (serialization logic can be added here)
        public static void SaveAccounts(string filename)
        {

            string jsonString = JsonSerializer.Serialize(accountList, new JsonSerializerOptions { WriteIndented = true });
        }

        public static void SaveUsers(string filename)
        {
            // Serialization logic for saving users to a file (e.g., JSON)
        }

        public static List<Transaction> GetAllTransactions()
        {
            var allTransactions = new List<Transaction>();
            foreach (var account in ACCOUNTS.Values)
            {
                allTransactions.AddRange(account.GetTransactions());
            }
            return allTransactions;
        }
    }
}