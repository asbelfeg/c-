using System;
using System.Collections.Generic;
using System.Linq;
using LibraryApp;

namespace LibraryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<LibraryItem> library = new List<LibraryItem>();

            Console.WriteLine("=== Библиотека: Добавление элементов ===");
            Console.WriteLine("Типы: B - Книга, M - Журнал, D - DVD. Для завершения: DONE");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (input.Equals("DONE", StringComparison.OrdinalIgnoreCase))
                {
                    if (library.Count < 8)
                    {
                        Console.WriteLine($"\n Добавлено {library.Count} элементов. Нужно минимум 8.");
                        continue;
                    }
                    break;
                }

                string[] parts = input.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                {
                    Console.WriteLine("Неверный формат ввода. Используйте: [Тип] [Название]");
                    continue;
                }

                string type = parts[0].ToUpper();
                string title = parts[1];
                LibraryItem item = null;

                switch (type)
                {
                    case "B": item = new Book(title); break;
                    case "M": item = new Magazine(title); break;
                    case "D": item = new Dvd(title); break;
                    default: Console.WriteLine($"Неизвестный тип: {type}"); continue;
                }

                library.Add(item);
                Console.WriteLine($" Добавлен: {item.GetType().Name} - {item.Title}");
            }

            Console.WriteLine("\n--- Ввод завершен. Всего элементов: " + library.Count + " ---");

            DisplayItemCounts(library);

            Console.WriteLine("\n=== Результат поиска (Полиморфизм) ===");
            CallSearchOnFirstFive(library);
        }

        private static void DisplayItemCounts(List<LibraryItem> library)
        {
            var counts = library
                .GroupBy(item => item.GetType().Name)
                .Select(group => new { Type = group.Key, Count = group.Count() });

            Console.WriteLine("\n## Количество элементов по типу:");
            foreach (var count in counts)
            {
                Console.WriteLine($"- {count.Type}: {count.Count} шт.");
            }
        }

        private static void CallSearchOnFirstFive(List<LibraryItem> library)
        {
            var firstFive = library.Take(5);
            int index = 1;

            foreach (var item in firstFive)
            {
                if (item is ISearchable searchableItem)
                {
                    string result = searchableItem.Search();
                    Console.WriteLine($"{index++}. {result}");
                }
            }
        }
    }
}