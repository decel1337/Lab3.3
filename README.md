# Lab 3.3 — Варіант 1

Рішення `Lab33.sln` (.NET 8):

- `Lab33.Part1` — частина 1: клас `Student` (квиток, ПІБ, рік народження, група), 3 об'єкти,
  бінарна/XML/JSON/користувацька (ISerializable) серіалізація, порівняння масиву та `List<Student>`.
- `Lab33.DAL` — сутності, провайдери (Binary/Xml/Json/Custom), `EntityContext<T>`.
- `Lab33.BLL` — моделі, сервіси, винятки (без Console/Stream).
- `Lab33.PL` — меню та введення; перехоплює винятки BLL.
- `Lab33.App` — `Program.cs`, точка входу (`Menu.MainMenu()`).

Запуск: `dotnet run --project Lab33.Part1` або `dotnet run --project Lab33.App`.
