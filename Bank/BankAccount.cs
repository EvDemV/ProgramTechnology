using System;
using System.Collections.Generic;
using System.Text;

namespace Bank;

// BankAccount - потомок класса object => можно переопределить виртуальные методы, находящиеся в object
public class BankAccount
{
    static private int s_accountNuberSeed = 1000000000;
    public string Number { get; }
    public string Owner { get; private set; }

    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }
            return balance;
        }
    }

    private List<Transaction> _allTransactions = new List<Transaction>();

    public BankAccount(string name, decimal initialBalance)
    {
        Owner = name;
        MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
        Number = s_accountNuberSeed.ToString();
        s_accountNuberSeed++;
    }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
        }

        if (Balance < amount)
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdawal");
        }

        var withdrawal = new Transaction(-amount, date, note);
        _allTransactions.Add(withdrawal);
    }

    // Метод генерации истории счета (вывод таблицы транзакций)
    public string GetAccountHistory()
    {
        var report = new StringBuilder();
        decimal balance = 0;

        // Заголовок таблицы
        report.AppendLine("Date\t\tAmount\tBalance\tNote");

        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");
        }

        return report.ToString();
    }

    // Ключевое слово virtual позволяет в дочернем классе
    // предоставить другую реализацию метода PerformMonthAndTransactions()
    public virtual void PerformMonthAndTransitions()
    {
    }

    // Переопределяем метод, который унаследовали от object
    // Этот метод должен возвращать строку с состоянием объекта
    public override string ToString()
        => $"Type: {GetType().Name}\t" +
           $"Owner: {Owner}\t" +
           $"Number of account: {Number}\t" +
           $"Balance: {Balance}";
}
