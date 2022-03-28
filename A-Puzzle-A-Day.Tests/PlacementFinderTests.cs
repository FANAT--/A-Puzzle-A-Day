using System;
using System.Diagnostics;
using A_Puzzle_A_Day_Solver;
using NUnit.Framework;

namespace A_Puzzle_A_Day.Tests
{
    public class PlacementFinderTests
    {
        //[Test]
        //public void Find()
        //{
        //    // 11 января
        //    var placements = PlacementFinder.Find(0, 0, 3, 3);

        //    Assert.NotNull(placements);

        //    Console.WriteLine(Desk.ToString(placements));
        //}

        [Test]
        public void FindAll()
        {   
            // 11 января
            var result = PlacementFinder.FindAll(0, 0, 3, 3);

            foreach (var placements in result)
                Console.WriteLine(Desk.ToString(placements));

            Assert.AreEqual(74, result.Count);
        }

        [Test]
        public void Test_Performance()
        {
            // разогреваем
            var sw = new Stopwatch();

            for (var i = 0; i < 1; i++)
            {
                sw.Restart();
                PlacementFinder.FindAll(0, 0, 3, 3);
                sw.Stop();
            }
            
            // замеряем
            for (var i = 0; i < 5; i++)
            {
                sw.Restart();
                PlacementFinder.FindAll(0, 0, 3, 3);
                Console.WriteLine(sw.ElapsedMilliseconds + " ms");
            }

            // оригинал                                                                             5516 мс
            // храню Figure.Width/Height, а не вычисляю                                             1235 мс
            // убрал лишние проверки в CanPlaceFigure                                               1167 мс
            // Desk: строка как битовая маска                                                       1160 мс (ничего не изменилось, странно)
            // Figure: строка как битовая маска                                                     1179 мс (ничего не изменилось)
            // CanPlaceFigure, PlaceFigure - построчные расчеты                                      969 мс
            // Вся доска как битовая маска (long)                                                    964 мс
            // ClearFigure - построчно                                                               951 мс
            // CanPlaceFigure, PlaceFigure, ClearFigure - умножение заменил сложением                945 мс
            // передавать структуру FigurePlacement по ссылке как аргумент методов                   635 мс (!)
            // Figure - класс                                                                        281 мс (!)
            // рассчитать всевозможные FigurePlacement для каждой фигуры                             297 мс (чуть хуже)
            // В FigurePlacement хранить битовую маску для фигуры со смещением                        82 мс (!)
            // Завел FigurePlacementBitmask                                                           81 мс
            // Облегчил FigurePlacementBitmask и упразднил                                            80 мс
            // Разновидности фигуры - массив вместо List                                              66 мс

            // убрал лимит (расчетов делается больше)                                               1492 мс

            // [перенес на рабочий комп]                                                            2808 мс
            // убрал Template в PlacementFinder._placement                                          2730 мс
            // все позиции всех разновидностей фигур объединил в линейный массив                    1160 мс
            // изначально фильтровать позиции фигур, когда фигура не помещается на пустой доске      794 мс (!)

            // развернуть рекурсию (8 раз заинлайнить метод сам в себя)                              938 мс (стало хуже)
            // - развернул figuresPlacements поэлементно                                             875 мс
            // - убрал continue в циклах                                                             877 мс
            // - удалил _placements, т.к. вся информация есть в локальных переменных                1617 мс (стало хуже)
            // - разместил всю логику в методе FindAll                                              1132 мс
            // - массивы figuresPlacements перенес из кучи в стек (через stackalloc)                 793 мс
            // - _desk._bitmap перенес локально в метод                                              772 мс
            
            // ОТКЛОНЕНО
            // FigurePlacement - класс                                                 хуже
            // Desk - структура                                                        хуже (не факт)
        }

        [Test]
        public void FindMaxSolutions()
        {
            var minCount = int.MaxValue;
            int minCountMonth = 0, minCountDay = 0;

            for (var month = 1; month <= 12; month++)
            {
                Desk.TryGetMonthCell(month, out var monthRow, out var monthColumn);

                for (var day = 1; day <= 31; day++)
                {
                    Desk.TryGetDayCell(day, out var dayRow, out var dayColumn);
                    var results = PlacementFinder.FindAll(monthRow, monthColumn, dayRow, dayColumn);

                    if (results.Count < minCount)
                    {
                        minCount = results.Count;
                        minCountMonth = month;
                        minCountDay = day;
                    }
                }
            }

            Console.WriteLine(minCountDay + "." + minCountMonth + ": " + minCount);

            // 25 января: 216 решений (максимум)
            // 6 октября: 7 решений (минимум)
        }
    }
}
