using System.Text;
using Lab33.Part1;

Console.OutputEncoding = Encoding.UTF8;
Directory.CreateDirectory("output");

static void Check(string title, bool ok) => Console.WriteLine($"{(ok ? "[OK]  " : "[FAIL]")} {title}");
static long Size(string path) => new FileInfo(path).Length;

// 1-2. Масив із 3 об'єктів (кількість за варіантом)
Student[] students =
{
    new("2021001", "Іваненко Іван Іванович", 2004, 21),
    new("2021002", "Петренко Олена Сергіївна", 2005, 12),
    new("2021003", "Коваль Андрій Миколайович", 2003, 33),
};

Console.WriteLine("Вихідні дані:");
foreach (var s in students) s.PrintInfo();

var demo = new Student("0000000", "Демо Студент", 2004, 21);
demo.PromoteToNextCourse();
Console.WriteLine($"\nПісля переведення на наступний курс: {demo}\n");

// 3. Стандартна (бінарна) серіалізація масиву та відновлення в новий масив
Serializers.SaveBinary("output/students.bin", students);
Student[] restoredBin = Serializers.LoadBinary<Student[]>("output/students.bin");
Check("Бінарна: масив відновлено", students.SequenceEqual(restoredBin));

// 5. XML та JSON
Serializers.SaveXml("output/students.xml", students);
Check("XML: масив відновлено", students.SequenceEqual(Serializers.LoadXml<Student[]>("output/students.xml")));

Serializers.SaveJson("output/students.json", students);
Check("JSON: масив відновлено", students.SequenceEqual(Serializers.LoadJson<Student[]>("output/students.json")));

// 5. Користувацька серіалізація (ISerializable)
CustomStudent[] custom = students.Select(s => new CustomStudent(s)).ToArray();
Serializers.SaveBinary("output/students.custom.bin", custom);
CustomStudent[] restoredCustom = Serializers.LoadBinary<CustomStudent[]>("output/students.custom.bin");
Check("Користувацька (ISerializable): масив відновлено", custom.SequenceEqual(restoredCustom));

// 4. Серіалізація колекції List<Student> та порівняння з масивом
var list = new List<Student>(students);

Serializers.SaveBinary("output/list.bin", list);
Serializers.SaveXml("output/list.xml", list);
Serializers.SaveJson("output/list.json", list);

Console.WriteLine("\nКолекція List<Student> проти масиву:");
Check("Бінарна: вміст збігається", Serializers.LoadBinary<List<Student>>("output/list.bin").SequenceEqual(students));
Check("XML: вміст збігається", Serializers.LoadXml<List<Student>>("output/list.xml").SequenceEqual(students));
Check("JSON: вміст збігається", Serializers.LoadJson<List<Student>>("output/list.json").SequenceEqual(students));

Console.WriteLine("\nРозміри файлів (байт), масив / колекція:");
Console.WriteLine($"  binary: {Size("output/students.bin")} / {Size("output/list.bin")}");
Console.WriteLine($"  xml:    {Size("output/students.xml")} / {Size("output/list.xml")}");
Console.WriteLine($"  json:   {Size("output/students.json")} / {Size("output/list.json")}");
Console.WriteLine("\nВисновок: вміст однаковий; відрізняється службова розмітка " +
                  "(у XML кореневий елемент ArrayOfStudent для обох, у бінарному форматі — метадані типу).");
