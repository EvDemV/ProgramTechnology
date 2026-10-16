using System;

namespace Bank;

/// <summary>
/// Представляет журнал аудита для контроля операций.
/// </summary>
public class AuditLog
{
    public static void Log(string accountNumber, string operationType, decimal amount, DateTime date)
    {
        Console.WriteLine($"[AUDIT] Account: {accountNumber} | Type: {operationType} | Amount: {amount:F2} | Date: {date}");
    }
}
