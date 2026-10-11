namespace CompanyApp;

///<summary>
/// Абстрактный базовый класс, описывающий сотрудника компании.
/// </summary>
public abstract class Employee
{
    private readonly int _id;

    /// <summary>
    /// Уникальный идентификатор сотрудника.
    /// </summary>
    protected int Id => _id;

    /// <summary>
    /// Полное имя сотрудника.
    /// </summary>
    public string FullName {  get; set; }

    /// <summary>
    /// Месячный оклад сотрудника.
    /// </summary>
    public decimal Salary { get; set; }

    /// <summary>
    /// Квалификационный уровень сотрудника. Значение по умолчанию - Junior.
    /// </summary>
    public EmployeeLevel Level { get; set; } = EmployeeLevel.Junior;

    /// <summary>
    /// Вычисляемое свойство: годовой доход сотрудника (оклад * 12 месяцев).
    /// </summary>
    public decimal AnnualSalary => Salary * 12;

    /// <summary>
    /// Инициализирует сотрудника с полным набором данных.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="fullName">Полное имя.</param>
    /// <param name="salary">Оклад.</param>
    protected Employee(int id, string fullName, decimal salary)
    {
        _id = id;
        FullName = fullName;
        Salary = salary;
    }

    /// <summary>
    /// Инициализирует сотрудника только по имени, вызывая основной конструктор через this.
    /// </summary>
    /// <param name="fullName">Полное имя.</param>
    protected Employee(string fullName) : this(0, fullName, 0)
    {
    }

    ///<summary>
    /// Выполняет должностные обязанности(должен быть переопределен в наследниках).
    /// </summary>
    public abstract void Work();
}