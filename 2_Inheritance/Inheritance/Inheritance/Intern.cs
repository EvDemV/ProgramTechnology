using System.Numerics;

namespace CompanyApp;

/// <summary>
///  Описывает сотрудника-стажера.
/// </summary>
public class Intern : Employee
{
    /// <summary>
    /// Университет, в котором обучается стажер.
    /// </summary>
    public string University { get; set; }

    /// <summary>
    /// Вычисляемое свойство: является ли стажировка оплачиваемой.
    /// </summary>
    public bool IsPaid => Salary > 0;
    
    /// <summary>
    /// Создает экземпляр стажера.
    /// </summary>
    public Intern(int id, string fullName, decimal salary, string university)
        : base(id, fullName, salary)
    {
        University = university;
    }

    /// <summary>
    /// Реализация абстактного метода работы для стажера.
    /// </summary>
    public override void Work()
    {
        Console.WriteLine($"{FullName} learns technologies and helps the team.");
    }

    /// <summary>
    /// Переопределяет текстовое представление информации о стажере.
    /// </summary>
    public override string ToString()
    {
        return $"[Intern] {FullName}, {Salary}, university: {University}";
    }
}
