namespace Bank;

/// <summary>
/// Определяет клиентские статусы обслуживания в банке.
/// </summary>
public enum BankStatus
{
    /// <summary>
    /// Обычный статус (фиксированная комиссия).
    /// </summary>
    Regular,

    /// <summary>
    /// Премиальный статус (повышенный процент, без комиссии).
    /// </summary>
    Premium,

    /// <summary>
    /// VIP-статус (максимальный процент, кэшбэк, без комиссии).
    /// </summary>
    VIP
}
