namespace UniversityApp
{
    /// <summary>
    /// Репозиторий для чтения данных из CSV-файлов.
    /// </summary>
    public class CsvRepository
    {
        private string _basePath;

        public CsvRepository(string basePath)
        {
            _basePath = basePath;
        }

        /// <summary>
        /// Читает факультеты из файла faculties.csv
        /// </summary>
        public List<Faculty> GetFaculties()
        {
            List<Faculty> result = new List<Faculty>();

            string path = Path.Combine(_basePath, "faculties.csv");

            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);

            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string[] parts = lines[i].Split(',');

                if (parts.Length < 3) continue;

                Faculty f = new Faculty(
                    int.Parse(parts[0].Trim()),
                    parts[1].Trim(),
                    parts[2].Trim()
                );

                result.Add(f);
            }
            return result;
        }

        /// <summary>
        /// Читает академические группы из файла groups.csv
        /// </summary>
        public List<Group> GetGroups()
        {
            List<Group> result = new List<Group>();
            string path = Path.Combine(_basePath, "groups.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                string[] parts = lines[i].Split(',');
                if (parts.Length < 4) continue;

                Group g = new Group(
                    int.Parse(parts[0].Trim()),
                    parts[1].Trim(),
                    int.Parse(parts[2].Trim()),
                    int.Parse(parts[3].Trim())
                );
                result.Add(g);
            }
            return result;
        }

        /// <summary>
        /// Читает студентов из файла students.csv
        /// </summary>
        public List<Student> GetStudents()
        {
            List<Student> result = new List<Student>();
            string path = Path.Combine(_basePath, "students.csv");
            if (!File.Exists(path)) return result;

            string[] lines = File.ReadAllLines(path);
            if (lines.Length < 2) return result;

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                string[] parts = lines[i].Split(',');
                if (parts.Length < 5) continue;

                Student s = new Student(
                    int.Parse(parts[0].Trim()),
                    parts[1].Trim(),
                    int.Parse(parts[2].Trim()),
                    int.Parse(parts[3].Trim()),
                    decimal.Parse(parts[4].Trim())
                );
                result.Add(s);
            }
            return result;
        }
    }
}