using System.Collections.Generic;

namespace A_Puzzle_A_Day_Solver
{
    public static unsafe class PlacementFinder
    {
        /// <summary>
        /// Находит все подходящие размещения фигур на доске (перебором)
        /// </summary>
        public static List<long[]> FindAll(int markedRow1, int markedColumn1, int markedRow2, int markedColumn2)
        {
            // создаем достку с двумя занятыми ячейками
            var desk = Desk.Create(markedRow1, markedColumn1, markedRow2, markedColumn2);

            // получаем разновидности каждой фигуры
            // (здесь идет работа с объектами в куче)
            var figure0KindsPlacements = Desk.GetFigureKindsPlacements(desk, Figure.Bundle[0], out var figure0PlacementsCount);
            var figure1KindsPlacements = Desk.GetFigureKindsPlacements(desk, Figure.Bundle[1], out var figure1PlacementsCount);
            var figure2KindsPlacements = Desk.GetFigureKindsPlacements(desk, Figure.Bundle[2], out var figure2PlacementsCount);
            var figure3KindsPlacements = Desk.GetFigureKindsPlacements(desk, Figure.Bundle[3], out var figure3PlacementsCount);
            var figure4KindsPlacements = Desk.GetFigureKindsPlacements(desk, Figure.Bundle[4], out var figure4PlacementsCount);
            var figure5KindsPlacements = Desk.GetFigureKindsPlacements(desk, Figure.Bundle[5], out var figure5PlacementsCount);
            var figure6KindsPlacements = Desk.GetFigureKindsPlacements(desk, Figure.Bundle[6], out var figure6PlacementsCount);
            var figure7KindsPlacements = Desk.GetFigureKindsPlacements(desk, Figure.Bundle[7], out var figure7PlacementsCount);

            // ..и собираем их в линейные массивы, размещенные в стеке
            // (здесь идет работа со стеком)
            var figure0Placements = stackalloc long[figure0PlacementsCount];
            Copy(figure0KindsPlacements, figure0Placements);

            var figure1Placements = stackalloc long[figure1PlacementsCount];
            Copy(figure1KindsPlacements, figure1Placements);

            var figure2Placements = stackalloc long[figure2PlacementsCount];
            Copy(figure2KindsPlacements, figure2Placements);

            var figure3Placements = stackalloc long[figure3PlacementsCount];
            Copy(figure3KindsPlacements, figure3Placements);

            var figure4Placements = stackalloc long[figure4PlacementsCount];
            Copy(figure4KindsPlacements, figure4Placements);

            var figure5Placements = stackalloc long[figure5PlacementsCount];
            Copy(figure5KindsPlacements, figure5Placements);

            var figure6Placements = stackalloc long[figure6PlacementsCount];
            Copy(figure6KindsPlacements, figure6Placements);

            var figure7Placements = stackalloc long[figure7PlacementsCount];
            Copy(figure7KindsPlacements, figure7Placements);

            var result = new List<long[]>();

            // перебираем возможные положения разновидностей следующей фигуры
            for (var i0 = 0; i0 < figure0PlacementsCount; i0++)
            {
                if ((desk & figure0Placements[i0]) == 0)
                {
                    desk |= figure0Placements[i0];

                    // перебираем возможные положения разновидностей следующей фигуры
                    for (var i1 = 0; i1 < figure1PlacementsCount; i1++)
                    {
                        if ((desk & figure1Placements[i1]) == 0)
                        {
                            desk |= figure1Placements[i1];

                            // перебираем возможные положения разновидностей следующей фигуры
                            for (var i2 = 0; i2 < figure2PlacementsCount; i2++)
                            {
                                if ((desk & figure2Placements[i2]) == 0)
                                {
                                    desk |= figure2Placements[i2];

                                    // перебираем возможные положения разновидностей следующей фигуры
                                    for (var i3 = 0; i3 < figure3PlacementsCount; i3++)
                                    {
                                        if ((desk & figure3Placements[i3]) == 0)
                                        {
                                            desk |= figure3Placements[i3];

                                            // перебираем возможные положения разновидностей следующей фигуры
                                            for (var i4 = 0; i4 < figure4PlacementsCount; i4++)
                                            {
                                                if ((desk & figure4Placements[i4]) == 0)
                                                {
                                                    desk |= figure4Placements[i4];

                                                    // перебираем возможные положения разновидностей следующей фигуры
                                                    for (var i5 = 0; i5 < figure5PlacementsCount; i5++)
                                                    {
                                                        if ((desk & figure5Placements[i5]) == 0)
                                                        {
                                                            desk |= figure5Placements[i5];

                                                            // перебираем возможные положения разновидностей следующей фигуры
                                                            for (var i6 = 0; i6 < figure6PlacementsCount; i6++)
                                                            {
                                                                if ((desk & figure6Placements[i6]) == 0)
                                                                {
                                                                    desk |= figure6Placements[i6];

                                                                    // перебираем возможные положения разновидностей следующей фигуры
                                                                    for (var i7 = 0; i7 < figure7PlacementsCount; i7++)
                                                                    {
                                                                        if ((desk & figure7Placements[i7]) == 0)
                                                                        {
                                                                            desk |= figure7Placements[i7];

                                                                            // разместили все фигуры
                                                                            // сохраняем копию текущего размещения
                                                                            result.Add(new[]
                                                                            {
                                                                                figure0Placements[i0],
                                                                                figure1Placements[i1],
                                                                                figure2Placements[i2],
                                                                                figure3Placements[i3],
                                                                                figure4Placements[i4],
                                                                                figure5Placements[i5],
                                                                                figure6Placements[i6],
                                                                                figure7Placements[i7]
                                                                            });

                                                                            // Снимаем фигуру и пробуем дальше
                                                                            desk &= ~figure7Placements[i7];
                                                                        }
                                                                    }

                                                                    // Снимаем фигуру и пробуем дальше
                                                                    desk &= ~figure6Placements[i6];
                                                                }
                                                            }

                                                            // Снимаем фигуру и пробуем дальше
                                                            desk &= ~figure5Placements[i5];
                                                        }
                                                    }

                                                    // Снимаем фигуру и пробуем дальше
                                                    desk &= ~figure4Placements[i4];
                                                }
                                            }

                                            // Снимаем фигуру и пробуем дальше
                                            desk &= ~figure3Placements[i3];
                                        }
                                    }

                                    // Снимаем фигуру и пробуем дальше
                                    desk &= ~figure2Placements[i2];
                                }
                            }

                            // Снимаем фигуру и пробуем дальше
                            desk &= ~figure1Placements[i1];
                        }
                    }

                    // Снимаем фигуру и пробуем дальше
                    desk &= ~figure0Placements[i0];
                }
            }

            return result;
        }

        private static void Copy(List<long>[] source, long* destination)
        {
            var offset = 0;
            foreach (var figureKindPlacements in source)
                foreach (var placement in figureKindPlacements)
                    destination[offset++] = placement;
        }
    }
}
