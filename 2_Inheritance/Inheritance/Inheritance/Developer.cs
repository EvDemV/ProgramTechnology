namespace CompanyApp;

/// <summary>
/// Описывает сотрудника-разработчика.
/// </summary>
public class Developer : Employee
{
    /// <summary>
    /// Основной язык программирования разработчика.
    /// </summary>
    public string PrimaryLanguage { get; set; }

    /// <summary>
    /// Вычисляемое свойство: является ли разработчик старшим специалистом.
    /// </summary>
    public bool IsSenior => Level == EmployeeLevel.Senior;

    /// <summary>
    /// Создает экземпляр разработчика.
    /// </summary>
    public Developer(int id, string fullName, decimal salary, string primaryLanguage)
        : base(id, fullName, salary)
    {
        PrimaryLanguage = primaryLanguage;
    }

    /// <summary>
    /// Реализация абстрактного метода работы для разработчика.
    /// </summary>
    public override void Work()
    {
        Console.WriteLine($"{FullName} writes code.");
    }

    /// <summary>
    /// Переопределяет текстовое предоставление информации о разработчике.
    /// </summary>
    public override string ToString()
    {
        return $"[Developer] {FullName}, {Salary}, level: {Level}, language: {PrimaryLanguage}";
    }
}
