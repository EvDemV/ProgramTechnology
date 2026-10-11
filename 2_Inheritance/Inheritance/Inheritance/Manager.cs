namespace CompanyApp;

/// <summary>
/// Описывает сотрудника-менеджера.
/// </summary>
public class Manager : Employee, IManageable
{
    /// <summary>
    /// Размер управляемой команды.
    /// </summary>
    public int TeamSize { get; set; }

    /// <summary>
    /// Вычисляемое свойство: считается ли команда большой(более 5 чел).
    /// </summary>
    public bool IsBigTeam => TeamSize > 5;

    /// <summary>
    /// Создает экземпляр менеджера.
    /// </summary>
    public Manager(int id, string fullName, decimal salary, int teamSize)
        : base(id, fullName, salary)
    {
        TeamSize = teamSize;
    }

    /// <summary>
    /// Реализация абстрактного метода работы для менеджера.
    /// </summary>
    public override void Work()
    {
        Console.WriteLine($"{FullName} works on tasks.");
    }
    
    /// <summary>
    /// Реализация интерфейса управления.
    /// </summary>
    public void Manage()
    {
        Console.WriteLine($"{FullName} manages the team of {TeamSize}.");
    }

    /// <summary>
    /// Переопределяет текстовое представление информации о менеджере.
    /// </summary>
    public override string ToString()
    {
        return $"[Manager] {FullName}, {Salary}, level: {Level}, team: {TeamSize}.";
    }
}
