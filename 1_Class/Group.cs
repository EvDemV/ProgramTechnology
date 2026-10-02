namespace UniversityApp
{
    /// <summary>
    /// Представляет академическую группу.
    /// </summary>
    public class Group
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int FacultyId { get; private set; }
        public int Course { get; private set; }

        /// <summary>
        /// Конструктор класса Group с валидацией данных.
        /// </summary>
        public Group(int id, string name, int facultyId, int course)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя группы не может быть пустым.");
            }

            if (course < 1 || course > 6)
            {
                throw new ArgumentException("Курс должен быть в диапазоне от 1 до 6.");
            }

            Id = id;
            Name = name.Trim();
            FacultyId = facultyId;
            Course = course;
        }

        public bool IsSenior
        {
            get { return Course >= 4; }
        }

        public string GetInfo()
        {
            return $"{Name} ({Course} курс)";
        }
    }
}