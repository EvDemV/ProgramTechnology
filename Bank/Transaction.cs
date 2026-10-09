using System;

namespace Bank;

/// <summary>
/// Описывает неизменяемую запись финансовой транзакции.
/// </summary>
/// <param name="Amount">Сумма изменения баланса (положительная для депозита, отрицательная для списания).</param>
/// <param name="Date">Дата и время проведения транзакции.</param>
/// <param name="Note">Комментарий или основание для проведения операции.</param>
public record Transaction(decimal Amount, DateTime Date, string Note);
