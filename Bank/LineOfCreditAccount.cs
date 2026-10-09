namespace Bank;

/// <summary>
/// Представляет кредитный счёт с возможностью ухода баланса в минус до определенного лимита.
/// </summary>
public class LineOfCreditAccount : BankAccount
{
    /// <summary>
    /// Инициализирует новый экземпляр кредитного счёта с установленным кредитным лимитом.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="creditLimit">Размер кредитного лимита (положительное число).</param>
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit)
        : base(name, initialBalance, -creditLimit)
    {

    }
}
