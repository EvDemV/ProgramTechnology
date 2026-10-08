using System;

namespace Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount("Yana", 100000);
            BankAccount account2 = new BankAccount("Lena", 10);
            Console.WriteLine($"account {account1.Balance} №{account1.Number} {account1.Owner}");
            Console.WriteLine($"account {account2.Balance} №{account2.Number} {account2.Owner}");

            account1.MakeDeposit(2000000, DateTime.UtcNow, ":)");
            Console.WriteLine(account1.Balance);
            account1.MakeWithdrawal(200, DateTime.UtcNow, ":(");
            Console.WriteLine(account1.Balance);

            try
            {
                account2.MakeWithdrawal(10000, DateTime.UtcNow, ":(");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            // Блок тестирования дочернего класса с доски
            InterestEarningAccount interestEarning = new InterestEarningAccount("Yana", 1000m);
            interestEarning.MakeDeposit(1000m, DateTime.UtcNow, ";)");
            interestEarning.MakeWithdrawal(10m, DateTime.UtcNow, ":(");
            interestEarning.PerformMonthAndTransitions();

            Console.WriteLine(interestEarning); // автоматически вызывает ToString()
            Console.WriteLine(interestEarning.GetAccountHistory());

            LineOfCreditAccount lineOfCredit = new LineOfCreditAccount("Yana", 1000m, 1000m);
            lineOfCredit.MakeWithdrawal(500m, DateTime.UtcNow, "credit");

            GiftCardAccount giftcart = new GiftCardAccount("Yana", 1000m, 5000m);

            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(account1);
            accounts.Add(interestEarning);
            accounts.Add(lineOfCredit);
            accounts.Add(giftcart);

            foreach (BankAccount account in accounts)
            {
                Console.WriteLine(account);
                account.PerformMonthAndTransitions();
                Console.WriteLine(account.GetAccountHistory());
            }

            lineOfCredit.MakeWithdrawal(600m, DateTime.UtcNow, "credit");
            Console.WriteLine(lineOfCredit.GetAccountHistory());



        }
    }
}
