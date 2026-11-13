using System.Text;
using Lab33.BLL.Services;

namespace Lab33.PL;

public static class Menu
{
    public static void MainMenu()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var studentService = new StudentService();
        var students = new StudentMenu(studentService);
        var teachers = new TeacherMenu(new TeacherService());
        var astronauts = new AstronautMenu(new AstronautService());

        while (true)
        {
            Console.WriteLine("\n=== Головне меню ===");
            Console.WriteLine("1. Студенти");
            Console.WriteLine("2. Вчителі");
            Console.WriteLine("3. Астронавти");
            Console.WriteLine("0. Вихід");

            switch (ConsoleInput.ReadInt("Ваш вибір", 0, 3))
            {
                case 1: students.Run(); break;
                case 2: teachers.Run(); break;
                case 3: astronauts.Run(); break;
                case 0: return;
            }
        }
    }
}
