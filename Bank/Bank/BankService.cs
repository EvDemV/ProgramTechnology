using System;

namespace Bank;

/// <summary>
/// Сервис для проведения межбанковских операций.
/// </summary>
internal class BankService
{
    public static void Transfer(BankAccount source, BankAccount destination, decimal amount)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (destination == null) throw new ArgumentNullException(nameof(destination));

        DateTime transferDate = DateTime.UtcNow;

        // Списываем со счета А
        source.MakeWithdrawal(amount, transferDate, $"Transfer to {destination.Number}");

        try
        {
            // Пробуем зачислить на счет Б
            destination.MakeDeposit(amount, transferDate, $"Transfer from {source.Number}");
        }
        catch (Exception)
        {
            // Откат назад, если зачисление сломалось
            source.MakeDeposit(amount, transferDate, "ROLLBACK: Transfer failed");
            throw new InvalidOperationException("Transfer process crashed. Funds rolled back.");
        }
    }
}
