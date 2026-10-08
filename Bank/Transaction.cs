using System;

namespace Bank;

// мы создали неизменяемый тип данных благодаря record
public record Transaction(decimal Amount, DateTime Date, string Note);
