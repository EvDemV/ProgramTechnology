using System;
using System.Collections.Generic;
using System.Text;

namespace Bank;

/// <summary>
/// Представляет базовый класс банковского счёта.
/// </summary>
public class BankAccount
{
    /// <summary>
    /// Минимально допустимый баланс для данного типа счёта.
    /// </summary>
    public readonly decimal _minimumBalance;

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
    /// Инициализирует новый экземпляр счёта с нулевым минимальным балансом.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр счёта с заданным минимальным балансом и совершает начальный депозит.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="minimumBalance">Лимит минимального остатка.</param>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Owner = name;
        Number = s_accountNuberSeed.ToString();
        s_accountNuberSeed++;

        _minimumBalance = minimumBalance;

        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
    }

    /// <summary>
    /// Пополняет счёт на указанную сумму.
    /// </summary>
    /// <param name="amount">Сумма пополнения. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">Бросается, если сумма меньше или равна нулю.</exception>
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    /// <summary>
    /// Снимает указанную сумму со счёта и проверяет лимит овердрафта, и начисляет штраф при его превышении.
    /// </summary>
    /// <param name="amount">Сумма снятия. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">Бросается, если сумма меньше или равна нулю.</exception>
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        Transaction? overdraftTransaction
            = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? wthdrawal = new(-amount, date, note);
        _allTransactions.Add(wthdrawal);

        if (overdraftTransaction != null)
            _allTransactions.Add(overdraftTransaction);
    }

    /// <summary>
    /// Проверяет, превышен ли лимит снятия средств, и возвращает транзакцию штрафа в случае овердрафта.
    /// </summary>
    /// <param name="isOverdrawn">Флаг, указывающий на выход за пределы допустимого баланса.</param>
    /// <returns>Объект транзакции штрафа, если баланс нарушен; иначе — null.</returns>
    /// <exception cref="InvalidOperationException">Бросается, если на счету недостаточно средств для совершения операции.</exception>
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("Not sufficient rubls for this withdrawal");
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// Формирует историю трансляций по счёту в виде форматированной строки и рассчитывает промежуточный баланс для каждой строки.
    /// </summary>
    /// <returns>Строковое представление таблицы истории транзакций.</returns>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();
        decimal balance = 0;

        report.AppendLine("Date\t\tAmount\tBalance\tNote");

        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");
        }

        return report.ToString();
    }

    /// <summary>
    /// Выполняет регламентные операции в конце месяца (переопределяется в классах-потомках).
    /// </summary>
    public virtual void PerformMonthAndTransitions()
    {
    }

    /// <summary>
    /// Возвращает текстовую строку, содержащую основные характеристики текущего состояния счёта.
    /// </summary>
    /// <returns>Строка с типом счёта, владельцем, номером и балансом.</returns>
    public override string ToString()
        => $"Type: {GetType().Name}\t" +
           $"Owner: {Owner}\t" +
           $"Number of account: {Number}\t" +
           $"Balance: {Balance}";
}
