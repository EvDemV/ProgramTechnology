namespace CompanyApp;

internal class Program
{
    private static void Main(string[] args)
    {
        //Создаем список сотрудников базового типа
        List<Employee> employees = new List<Employee>();

        //Создаем конкретных сотрудников-наследников
        Developer dev = new Developer(1, "Ivanov", 150000m, "C#");
        dev.Level = EmployeeLevel.Junior;

        Manager mgr = new Manager(2, "Petrov", 200000m, 8);
        mgr.Level = EmployeeLevel.Senior;

        Intern intern = new Intern(3, "Sidorov", 30000m, "NSTU");

        // Благодаря полиморфизму мы можем хранить из в одной коллекции базового типа.
        employees.Add(dev);
        employees.Add(mgr);
        employees.Add(intern);

        //Обходим всех сотрудников в цикле
        foreach (Employee emp in employees)
        {
            // Автоматически вызовется переопределенный ToString() для каждого класса
            Console.WriteLine(emp);
            Console.WriteLine($"  annual: {emp.AnnualSalary}");

            //проверка типа во время выполнения с помощью оператора is и pattern matching
            if (emp is IManageable manageableEmployee)
            {
                manageableEmployee.Manage();
            }
            else
            {
                Console.WriteLine($"{emp.FullName} cannot manage.");
            }

            Console.WriteLine();
        }
    }
}