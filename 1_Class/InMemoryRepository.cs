namespace UniversityApp
{
    /// <summary>
    /// Репозиторий тестовых данных в оперативной памяти.
    /// </summary>
    public class InMemoryRepository
    {
        private List<Faculty> _faculties;
        private List<Group> _groups;
        private List<Student> _students;

        public InMemoryRepository()
        {
            _faculties = new List<Faculty>
            {
                new Faculty(1, "Информатики", "Иванов И.И."),
                new Faculty(2, "Экономики", "Petrov P.P."),
                new Faculty(3, "Гуманитарный", "Сидорова С.С.")
            };

            _groups = new List<Group>
            {
                new Group(10, "ИС-21", 1, 2),
                new Group(11, "ИС-22", 1, 1),
                new Group(20, "ЭК-41", 2, 4),
                new Group(30, "ГМ-11", 3, 1)
            };

            _students = new List<Student>
            {
                new Student(101, "Иванов И.И.", 10, 20, 8000),
                new Student(102, "Петров П.П.", 10, 21, 6500),
                new Student(103, "Сидоров С.С.", 11, 19, 0),
                new Student(104, "Козлов К.К.", 20, 22, 12000),
                new Student(105, "Смирнова А.А.", 30, 18, 5000)
            };
        }

        public List<Faculty> GetFaculties() => _faculties;
        public List<Group> GetGroups() => _groups;
        public List<Student> GetStudents() => _students;
    }
}