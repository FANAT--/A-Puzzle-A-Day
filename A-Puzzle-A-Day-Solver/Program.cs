using System;
using System.Diagnostics;

namespace A_Puzzle_A_Day_Solver
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    var today = DateTime.Today;
                    WriteLine("Запустите программу с параметром дд.мм, например, " + AppDomain.CurrentDomain.FriendlyName +
                                      " " + today.Day.ToString("00") + "." + today.Month.ToString("00"));
                }
                else
                {
                    var arg = args[0];
                    if (!TryParseArg(arg, out var monthRow, out var monthColumn, out var dayRow, out var dayColumn))
                    {
                        WriteLine("Некорректный формат параметра: " + arg);
                    }
                    else
                    {
                        WriteLine("Выполняется поиск решений для " + arg + "...");
                        WriteLine();

                        var sw = Stopwatch.StartNew();
                        var result = PlacementFinder.FindAll(monthRow, monthColumn, dayRow, dayColumn);
                        sw.Stop();
                        
                        foreach (var placements in result)
                            WriteLine(Desk.ToString(placements));

                        WriteLine("Найдено решений: " + result.Count + " (за " + sw.Elapsed.TotalSeconds.ToString("0.0") + " сек)");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLine("Ошибка " + ex);
            }

            WriteLine();
            WriteLine("Нажмите любую клавишу, чтобы выйти...");
            Console.ReadKey();
        }

        private static bool TryParseArg(string arg, out int monthRow, out int monthColumn, out int dayRow, out int dayColumn)
        {
            monthRow = monthColumn = dayRow = dayColumn = 0;
            var dateParts = arg.Split('.');
            return dateParts.Length == 2 &&
                   int.TryParse(dateParts[0], out var day) &&
                   int.TryParse(dateParts[1], out var month) &&
                   Desk.TryGetMonthCell(month, out monthRow, out monthColumn) &&
                   Desk.TryGetDayCell(day, out dayRow, out dayColumn);
        }

        private static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        private static void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
