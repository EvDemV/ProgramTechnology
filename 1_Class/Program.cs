namespace UniversityApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Faculty> faculties = null;
            List<Group> groups = null;
            List<Student> students = null;

            Console.WriteLine("Выберите источник данных:");
            Console.WriteLine("1 - Загрузка из InMemoryRepository");
            Console.WriteLine("2 - Загрузка из CsvRepository(\"data\")");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
                       
            try
            {
                switch (choice)
                {
                    case "1":
                        InMemoryRepository inMemory = new InMemoryRepository();
                        faculties = inMemory.GetFaculties();
                        groups = inMemory.GetGroups();
                        students = inMemory.GetStudents();
                        break;
                    case "2":
                        string folderPath = "data";
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            System.IO.Directory.CreateDirectory(folderPath);
                        }
                        CsvRepository csv = new CsvRepository(folderPath);

                        faculties = csv.GetFaculties();
                        groups = csv.GetGroups();
                        students = csv.GetStudents();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор");
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Критическая ошибка при загрузке данных]: {ex.Message}");
                Console.WriteLine("Проверьте правильность формата данных в CSV-файлах.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\n ТЕСТИРОВАНИЕ МЕТОДОВ ПРОГРАММЫ \n");

            // 1. Поиск группы студента по имени
            string targetStudent = "Иванов И.И.";
            Group foundGroup = FindGroup(targetStudent, students, groups);
            Console.Write($"1. FindGroup(\"{targetStudent}\"): ");
            Console.WriteLine(foundGroup != null ? foundGroup.GetInfo() : "null");

            string unknownStudent = "Неизвестный студент";
            Group notFoundGroup = FindGroup(unknownStudent, students, groups);
            Console.WriteLine($"   FindGroup(\"{unknownStudent}\") → {(notFoundGroup != null ? notFoundGroup.GetInfo() : "null")}");

            // 2. Поиск факультета группы
            if (foundGroup != null)
            {
                Faculty foundFaculty = FindFaculty(foundGroup, faculties);
                Console.Write($"2. FindFaculty(group \"{foundGroup.Name}\"): ");
                Console.WriteLine(foundFaculty != null ? foundFaculty.Info : "null");
            }

            // 3. Суммарная стипендия
            decimal totalScholarship = GetTotalScholarship(students);
            Console.WriteLine($"3. GetTotalScholarship: {totalScholarship} руб.");

            // 4. Студенты со стипендией выше порога (Пузырьковая сортировка по убыванию)
            decimal threshold = 5000;
            List<Student> richStudents = GetStudentsWithHighScholarship(students, threshold);
            Console.Write($"4. GetStudentsWithHighScholarship({threshold}): ");
            for (int i = 0; i < richStudents.Count; i++)
            {
                Console.Write($"{richStudents[i].FullName} ({richStudents[i].Scholarship})");
                if (i < richStudents.Count - 1) Console.Write(", ");
            }
            Console.WriteLine();

            // 5. Вывод всех студентов по формату
            Console.WriteLine("\n5. PrintAllStudents:");
            PrintAllStudents(students, groups, faculties);

            Console.ReadLine();
        }

        /// <summary>
        /// 1. Поиск группы студента по его полному имени (возвращает первую найденную).
        /// </summary>
        static Group FindGroup(string studentName, List<Student> students, List<Group> groups)
        {
            Student foundStudent = null;
            foreach (Student student in students)
            {
                if (student.FullName == studentName)
                {
                    foundStudent = student;
                    break;
                }
            }

            if (foundStudent == null) return null;

            foreach (Group group in groups)
            {
                if (group.Id == foundStudent.GroupId)
                {
                    return group;
                }
            }
            return null;
        }

        /// <summary>
        /// 2. Поиск факультета для конкретной группы.
        /// </summary>
        static Faculty FindFaculty(Group group, List<Faculty> faculties)
        {
            if (group == null) return null;

            foreach (Faculty faculty in faculties)
            {
                if (faculty.Id == group.FacultyId)
                {
                    return faculty;
                }
            }
            return null;
        }

        /// <summary>
        /// 3. Расчет суммарной стипендии всех студентов.
        /// </summary>
        static decimal GetTotalScholarship(List<Student> students)
        {
            if (students == null || students.Count == 0) return 0;

            decimal total = 0;
            foreach (Student student in students)
            {
                total += student.Scholarship;
            }
            return total;
        }

        /// <summary>
        /// 4. Возвращает список студентов со стипендией выше порога, отсортированный по убыванию (Пузырек).
        /// </summary>
        static List<Student> GetStudentsWithHighScholarship(List<Student> students, decimal threshold)
        {
            List<Student> filtered = new List<Student>();

            foreach (Student student in students)
            {
                if (student.Scholarship > threshold)
                {
                    filtered.Add(student);
                }
            }

            for (int i = 0; i < filtered.Count - 1; i++)
            {
                for (int j = 0; j < filtered.Count - i - 1; j++)
                {
                    if (filtered[j].Scholarship < filtered[j + 1].Scholarship)
                    {
                        Student temp = filtered[j];
                        filtered[j] = filtered[j + 1];
                        filtered[j + 1] = temp;
                    }
                }
            }

            return filtered;
        }

        /// <summary>
        /// 
        /// 5. Форматированный вывод всех студентов со связями.
        /// </summary>
        static void PrintAllStudents(List<Student> students, List<Group> groups, List<Faculty> faculties)
        {
            foreach (Student student in students)
            {
                Group studentGroup = null;
                foreach (Group g in groups)
                {
                    if (g.Id == student.GroupId)
                    {
                        studentGroup = g;
                        break;
                    }
                }

                Faculty studentFaculty = null;
                if (studentGroup != null)
                {
                    foreach (Faculty f in faculties)
                    {
                        if (f.Id == studentGroup.FacultyId)
                        {
                            studentFaculty = f;
                            break;
                        }
                    }
                }

                string groupInfo = studentGroup != null ? $"\"{studentGroup.GetInfo()}\"" : "—";
                string facultyName = studentFaculty != null ? $"\"{studentFaculty.Name}\"" : "—";

                Console.WriteLine($"\"{student.GetInfo()}\" — группа {groupInfo}, факультет {facultyName}");
            }
        }
    }
}

