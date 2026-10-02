namespace UniversityApp
{
    /// <summary>
    /// Представляет факультет университета.
    /// </summary>
    public class Faculty
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Dean { get; private set; }

        /// <summary>
        /// Конструктор класса Faculty с валидацией данных.
        /// </summary>
        public Faculty(int id, string name, string dean)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Название факультета не может быть пустым.");
            }

            if (string.IsNullOrWhiteSpace(dean))
            {
                throw new ArgumentException("Имя декана не может быть пустым.");
            }

            Id = id;
            Name = name.Trim();
            Dean = dean.Trim();
        }

        public string Info
        {
            get
            {
                return $"{Name} (декан: {Dean})";
            }
        }
    }
}