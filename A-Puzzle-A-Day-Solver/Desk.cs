using System;
using System.Collections.Generic;
using System.Text;

namespace A_Puzzle_A_Day_Solver
{
    /// <summary>
    /// Доска, на которой размещаются фигуры
    /// </summary>
    public static class Desk
    {
        public const int Size = 7;
        
        public static long Create()
        {
            //var desk = 0L; // битовая маска

            //desk = Mark(desk, 0, 6);
            //desk = Mark(desk, 1, 6);

            //desk = Mark(desk, 6, 3);
            //desk = Mark(desk, 6, 4);
            //desk = Mark(desk, 6, 5);
            //desk = Mark(desk, 6, 6);

            return 527765581340736L;
        }

        public static long Create(int markedRow1, int markedColumn1, int markedRow2, int markedColumn2)
        {
            //var desk = 0L; // битовая маска

            //desk = Mark(desk, 0, 6);
            //desk = Mark(desk, 1, 6);

            //desk = Mark(desk, 6, 3);
            //desk = Mark(desk, 6, 4);
            //desk = Mark(desk, 6, 5);
            //desk = Mark(desk, 6, 6);

            //desk = Mark(desk, markedRow1, markedColumn1);
            //desk = Mark(desk, markedRow2, markedColumn2);

            return 527765581340736L | (1L << (markedRow1 * Size + markedColumn1)) | (1L << (markedRow2 * Size + markedColumn2));
        }

        /// <summary>
        /// Занимает указанную ячейку
        /// </summary>
        //private static long Mark(long desk, int rowIndex, int columnIndex)
        //{
        //    var mask = 1L << (rowIndex * Size + columnIndex); // TODO лучше 8, тогда умножение будет быстрее
        //    return desk | mask;
        //}

        public static List<long>[] GetFigureKindsPlacements(long desk, Figure figure, out int allPlacementsCount)
        {
            // получаем разновидности фигуры
            var figureKinds = figure.GetKinds();

            // для каждой разновидности получаем ее всевозможные позиции на доске
            var figureKindsPlacements = new List<long>[figureKinds.Count];
            allPlacementsCount = 0;
            for (var i = 0; i < figureKinds.Count; i++)
            {
                var placements = GetFigurePlacements(desk, figureKinds[i]);
                allPlacementsCount += placements.Count;
                figureKindsPlacements[i] = placements;
            }
            return figureKindsPlacements;
        }

        /// <summary>
        /// Возвращает теоретические варианты размещения фигуры на доске
        /// </summary>
        private static List<long> GetFigurePlacements(long desk, Figure figure)
        {
            // генерируем всевозможные размещения фигуры на доске с учетом габаритов
            var rows = Size + 1 - figure.Height;
            var columns = Size + 1 - figure.Width;

            var placements = new List<long>(rows * columns);
            for (var row = 0; row < rows; row++)
                for (var column = 0; column < columns; column++)
                {
                    var figureMask = figure.GetMask(row, column);
                    if ((desk & figureMask) == 0) // фигуру в таком положении можно разместить на доске
                        placements.Add(figureMask);
                }
            return placements;
        }

        
        public static bool CanPlaceFigure(long desk, Figure figure, int row, int column)
        {
            // проверяем, что место для фигуры свободно
            // если есть пересечение битов, т.е. какие-то ячейки доски уже заняты
            return (desk & figure.GetMask(row, column)) == 0;
        }

        public static long PlaceFigure(long desk, Figure figure, int row, int column)
        {
            return desk | figure.GetMask(row, column);
        }


        public static string ToString(long[] placements)
        {
            var board = new char[Size * Size];
            for (var i = 0; i < board.Length; i++)
                board[i] = ' ';

            // рисуем каждую фигуру на доске буквами A, B, C, ...

            for (var i = 0; i < placements.Length; i++)
            {
                var figureTemplate = (char)('A' + i);
                var placementBitmask = placements[i];

                for (var j = 0; j < Size * Size; j++)
                {
                    // если i-й бит есть в маске, в соответствующей ячейке рисуем шаблон фигуры
                    var mask = 1L << j;
                    if ((placementBitmask & mask) == mask)
                        board[j] = figureTemplate;
                }
            }

            // рисуем границы фигур Unicode-символами: ─ │ ┌ ┐ └ ┘ ├ ┤ ┬ ┴ ┼

            var builder = new StringBuilder();
            for (var i = 0; i <= Size; i++)
            {
                for (var j = 0; j <= Size; j++)
                    builder.Append(GetCornerChar(board, i, j));
                builder.AppendLine();
            }
            return builder.ToString();
        }

        /// <summary>
        /// Рисует верхний левый угол указанной ячейки Unicode-символами: ─ │ ┌ ┐ └ ┘ ├ ┤ ┬ ┴ ┼
        /// </summary>
        private static string GetCornerChar(char[] board, int row, int column)
        {
            // рисуем верхний левый угол для указанной ячейки (ячейка может выходить за правую и нижнюю границы)
            const char absent = ' '; // отсутствующая ячейка

            var topLeft = row == 0 || column == 0 ? absent : board[(row - 1) * Size + column - 1];
            var topRight = row == 0 || column == Size ? absent : board[(row - 1) * Size + column];
            var bottomLeft = row == Size || column == 0 ? absent : board[row * Size + column - 1];
            var bottomRight = row == Size || column == Size ? absent : board[row * Size + column];

            if (topLeft == topRight)
            {
                if (bottomLeft == bottomRight)
                {
                    if (topLeft == bottomLeft)
                        return "  ";
                    else
                        return "──";
                }
                else // bottomLeft != bottomRight
                {
                    if (topLeft == bottomLeft)
                        return "┌─";
                    else if (topRight == bottomRight)
                        return "┐ ";
                    else
                        return "┬─";
                }
            }
            else // topLeft != topRight
            {
                if (bottomLeft == bottomRight)
                {
                    if (topLeft == bottomLeft)
                        return "└─";
                    else if (topRight == bottomRight)
                        return "┘ ";
                    else
                        return "┴─";
                }
                else // bottomLeft != bottomRight
                {
                    if (topLeft == bottomLeft)
                    {
                        if (topRight == bottomRight)
                            return "│ ";
                        else
                            return "├─";
                    }
                    else
                    {
                        if (topRight == bottomRight)
                            return "┤ ";
                        else
                            return "┼─";
                    }
                }
            }
        }

        public static bool TryGetMonthCell(int month, out int rowIndex, out int columnIndex)
        {
            if (month >= 1 && month <= 12)
            {
                rowIndex = Math.DivRem(month - 1, 6, out columnIndex);
                return true;
            }
            rowIndex = columnIndex = 0;
            return false;
        }

        public static bool TryGetDayCell(int day, out int rowIndex, out int columnIndex)
        {
            if (day >= 1 && day <= 31)
            {
                rowIndex = Math.DivRem(day - 1, 7, out columnIndex) + 2;
                return true;
            }
            rowIndex = columnIndex = 0;
            return false;
        }
    }
}
