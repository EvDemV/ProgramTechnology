using System;

namespace Bank;

/// <summary>
/// Представляет сберегательный счёт, на который начисляются проценты при превышении лимита остатка.
/// </summary>
public class InterestEarningAccount : BankAccount
{
    /// <summary>
    /// Инициализирует новый экземпляр сберегательного счёта.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    public InterestEarningAccount(string name, decimal initialBalance)
        : base(name, initialBalance)
    { }

    /// <summary>
    /// Рассчитывает и начисляет проценты на остаток в конце месяца, если баланс превышает 500 единиц.
    /// </summary>
    public override void PerformMonthAndTransitions()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "Apply month interest");
        }
    }
}
