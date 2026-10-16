using System;
using System.Collections.Generic;
using System.Text;

namespace Bank;

/// <summary>
/// Представляет банковский счёт физического лица.
/// </summary>
internal class BankAccount
{
    private static int s_accountNuberSeed = 1000000000;

    /// <summary>
    /// Уникальный номер счёта.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Имя владельца счёта.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    /// Текущий клиентский статус обслуживания счёта. Значение по умолчанию — Regular.
    /// </summary>
    public BankStatus Status { get; set; } = BankStatus.Regular;

    /// <summary>
    /// Текущий баланс счёта, вычисляемый на основе истории транзакций.
    /// </summary>
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

    /// <summary>
    /// Инициализирует новый экземпляр класса банковского счёта.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    public BankAccount(string name, decimal initialBalance)
    {
        Owner = name;
        Number = s_accountNuberSeed.ToString();
        s_accountNuberSeed++;

        // Первый депозит совершается без вызова аудита (как системное действие)
        if (initialBalance > 0)
        {
            var deposit = new Transaction(initialBalance, DateTime.UtcNow, "Initial balance");
            _allTransactions.Add(deposit);
        }
    }

    /// <summary>
    /// Пополняет счёт на указанную сумму.
    /// </summary>
    /// <param name="amount">Сумма пополнения.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);

        // Задача 2: Вызов аудита операций
        string opType = note.Contains("interest") || note.Contains("bonus") ? "Interest" : "Deposit";
        AuditLog.Log(Number, opType, amount, date);
    }

    /// <summary>
    /// Снимает указанную сумму со счёта.
    /// </summary>
    /// <param name="amount">Сумма снятия.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
        }

        if (Balance < amount)
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdrawal");
        }

        var withdrawal = new Transaction(-amount, date, note);
        _allTransactions.Add(withdrawal);

        // Задача 2: Вызов аудита операций
        AuditLog.Log(Number, "Withdrawal", amount, date);
    }

    /// <summary>
    /// Задача 1: Начисление комиссий, бонусов и кэшбэка в конце месяца в зависимости от статуса.
    /// </summary>
    public void PerformMonthAndTransitions()
    {
        if (Status == BankStatus.Regular)
        {
            MakeWithdrawal(50m, DateTime.UtcNow, "Regular status monthly fee");
        }
        else if (Status == BankStatus.Premium)
        {
            if (Balance > 10000m)
            {
                decimal interest = (Balance - 10000m) * 0.01m;
                MakeDeposit(interest, DateTime.UtcNow, "Premium status bonus interest");
            }
        }
        else if (Status == BankStatus.VIP)
        {
            if (Balance > 10000m)
            {
                decimal interest = (Balance - 10000m) * 0.03m;
                MakeDeposit(interest, DateTime.UtcNow, "VIP status bonus interest");
            }

            decimal totalWithdrawals = 0;
            foreach (var transaction in _allTransactions)
            {
                if (transaction.Amount < 0 && !transaction.Note.Contains("VIP Cashback"))
                {
                    totalWithdrawals += Math.Abs(transaction.Amount);
                }
            }

            if (totalWithdrawals > 0)
            {
                decimal cashback = totalWithdrawals * 0.05m;
                MakeDeposit(cashback, DateTime.UtcNow, "VIP Cashback benefit");
            }
        }
    }

    /// <summary>
    /// Формирует историю транзакций по счёту.
    /// </summary>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();
        decimal balance = 0;
        report.AppendLine("Data\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");
        }
        return report.ToString();
    }
}
