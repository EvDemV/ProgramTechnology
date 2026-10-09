using System;

namespace Bank;

/// <summary>
/// Представляет подарочный счёт со специальным ежемесячным фиксированным бонусом.
/// </summary>
public class GiftCardAccount : BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;

    /// <summary>
    /// Инициализирует новый экземпляр подарочного счёта.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="monthlyDeposit">Сумма ежемесячного пополнения (по умолчанию 0).</param>
    public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0)
        : base(name, initialBalance)
        => _monthlyDeposit = monthlyDeposit;

    /// <summary>
    /// Начисляет ежемесячный бонус на счёт, если сумма пополнения установлена.
    /// </summary>
    public override void PerformMonthAndTransitions()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add monthlydeposit");
        }
    }

    /// <summary>
    /// Возвращает строковое представление счёта, дополненное информацией о ежемесячном депозите.
    /// </summary>
    /// <returns>Строка с данными о счёте и суммой бонуса.</returns>
    public override string ToString() => base.ToString() + $"monthly deposit: {_monthlyDeposit}";
}
