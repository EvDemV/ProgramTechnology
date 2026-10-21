using System;

namespace Bank;

internal class Program
{
    private static void Main(string[] args)
    {
        BankAccount accountA = new BankAccount("Ivanov", 25000m);
        accountA.Status = BankStatus.Premium;

        BankAccount accountB = new BankAccount("Petrov", 5000m);
        accountB.Status = BankStatus.VIP;

        Console.WriteLine("\nВыполняем перевод денег (Задача 3)");
        BankService.Transfer(accountA, accountB, 5000m);

        Console.WriteLine("\nКонец месяца (Задача 1)");
        accountA.PerformMonthAndTransitions();
        accountB.PerformMonthAndTransitions();

        Console.WriteLine("\nИстория счета Петрова (VIP)");
        Console.WriteLine(accountB.GetAccountHistory());
    }
}
