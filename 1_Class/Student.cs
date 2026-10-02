namespace UniversityApp
{
    /// <summary>
    /// Представляет студента университета.
    /// </summary>
    public class Student
    {
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public int GroupId { get; private set; }
        public int Age { get; private set; }
        public decimal Scholarship { get; private set; }

        /// <summary>
        /// Конструктор класса Student с валидацией данных.
        /// </summary>
        public Student(int id, string fullName, int groupId, int age, decimal scholarship)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("ФИО студента не может быть пустым.");
            }

            if (scholarship < 0)
            {
                throw new ArgumentException("Стипендия не может быть меньше 0.");
            }

            if (age <= 0)
            {
                throw new ArgumentException("Возраст должен быть больше 0.");
            }

            Id = id;
            FullName = fullName.Trim();
            GroupId = groupId;
            Age = age;
            Scholarship = scholarship;
        }

        public bool HasScholarship
        {
            get { return Scholarship > 0; }
        }

        public string GetInfo()
        {
            return $"{FullName} ({Age} лет, стипендия {Scholarship})";
        }
    }
}