using System;
using System.Collections.Generic;
using System.Text;

namespace A_Puzzle_A_Day_Solver
{
    /// <summary>
    /// Фигура
    /// </summary>
    public partial class Figure : IEquatable<Figure>
    {
        private const int Size = 4;
        
        private readonly byte[] _rows; // 4 строки, каждая строка - битовая маска из 4-х ячеек

        public readonly int Width;
        public readonly int Height;

        public Figure(params byte[] points)
        {
            if (points.Length != Size * Size)
                throw new ArgumentException("Должно быть " + Size * Size + " точек", nameof(points));
            _rows = new byte[Size];

            for (var i = 0; i < Size; i++)
                _rows[i] = (byte)(points[i * Size] | (points[i * Size + 1] << 1) | (points[i * Size + 2] << 2) | (points[i * Size + 3] << 3));

            Width = 1;
            Height = 1;

            // первая строка и первый столбец должны быть непустыми
            if (IsRowEmpty(0))
                throw new ArgumentException("Первая строка пуста");
            if (IsColumnEmpty(0))
                throw new ArgumentException("Первый столбец пустой");

            while (Width < Size && !IsColumnEmpty(Width))
                Width++;

            while (Height < Size && !IsRowEmpty(Height))
                Height++;
        }

        public bool IsMarked(int rowIndex, int columnIndex)
        {
            var mask = (byte)(1 << columnIndex);
            return (_rows[rowIndex] & mask) == mask;
        }

        public long GetMask(int row, int column)
        {
            // рассчитываем битовую маску фигуры, расположенной на доске
            var result = 0L;
            var offset = row * Desk.Size + column; // TODO Size=8
            for (var i = 0; i < Height; i++, offset += Desk.Size)
                result |= (long)_rows[i] << offset;
            return result;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            var rowsExists = false;

            for (var i = 0; i < Size; i++)
            {
                var isEmpty = IsRowEmpty(i);
                if (isEmpty)
                    continue;

                if (rowsExists)
                    builder.AppendLine();

                for (var j = 0; j < Size; j++)
                {
                    var ch = IsMarked(i, j) ? 'X' : ' ';
                    builder.Append(ch);
                }
                // удаляем концевые пробелы
                var lastIndex = builder.Length - 1;
                while (builder[lastIndex] == ' ')
                    builder.Remove(lastIndex--, 1);

                rowsExists = true;
            }

            return builder.ToString();
        }

        private bool IsRowEmpty(int rowIndex)
        {
            for (var j = 0; j < Size; j++)
            {
                if (IsMarked(rowIndex, j))
                    return false;
            }
            return true;
        }

        private bool IsColumnEmpty(int columnIndex)
        {
            for (var i = 0; i < Size; i++)
            {
                if (IsMarked(i, columnIndex))
                    return false;
            }
            return true;
        }

        private static bool IsColumnEmpty(byte[] points, int columnIndex)
        {
            for (var i = 0; i < Size; i++)
            {
                if (points[i * Size + columnIndex] == 1)
                    return false;
            }
            return true;
        }
        
        public bool Equals(Figure other)
        {
            for (var i = 0; i < _rows.Length; i++)
                if (_rows[i] != other._rows[i])
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Figure other && Equals(other);
        }

        public override int GetHashCode()
        {
            // сворачиваем как битовую маску
            int result = _rows[0];
            for (var i = 1; i < _rows.Length; i++)
            {
                result <<= Size;
                result |= _rows[i];
            }

            return result;
        }

        /// <summary>
        /// Возвращает перевернутую фигуру
        /// </summary>
        public Figure GetTurned()
        {
            // ищем самый правый непустой столбец
            var lastColumnIndex = Width - 1;

            var points = new byte[Size * Size];
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j <= lastColumnIndex; j++)
                {
                    var from = j;
                    var to = lastColumnIndex - from;
                    if (IsMarked(i, from))
                        points[i * Size + to] = 1;
                }
            }

            return new Figure(points);
        }

        /// <summary>
        /// Возвращает фигуру, повернутую на 90 градусов по часовой стрелке
        /// </summary>
        public Figure GetRotated90()
        {
            var points = new byte[Size * Size];
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    if (IsMarked(i, j))
                    {
                        var newI = j;
                        var newJ = Size - 1 - i;
                        points[newI * Size + newJ] = 1;
                    }
                }
            }

            // убираем пустые столбцы слева, если они появились
            var columnStartIndex = 0;
            while (IsColumnEmpty(points, columnStartIndex))
                columnStartIndex++;
            if (columnStartIndex > 0)
            {
                for (var i = 0; i < Size; i++)
                for (var j = 0; j < Size; j++)
                {
                    if (j + columnStartIndex < Size)
                        points[i * Size + j] = points[i * Size + j + columnStartIndex];
                    else
                        points[i * Size + j] = 0;
                }
            }

            return new Figure(points);
        }

        /// <summary>
        /// Возвращает свои уникальные разновидности (повороты, переворот на другую сторону)
        /// </summary>
        public List<Figure> GetKinds()
        {
            // Фигуру можно поворачивать по часовой стрелке (4 раза),
            // а также переворачивать на другую сторону.
            // Таким образом, у фигуры может быть максимум 8 разновидностей

            var kinds = new List<Figure>();
            kinds.Add(this);

            var rotated90 = GetRotated90();
            if (!kinds.Contains(rotated90))
                kinds.Add(rotated90);

            var rotated180 = rotated90.GetRotated90();
            if (!kinds.Contains(rotated180))
                kinds.Add(rotated180);

            var rotated270 = rotated180.GetRotated90();
            if (!kinds.Contains(rotated270))
                kinds.Add(rotated270);

            var turned = GetTurned();
            if (kinds.Contains(turned))
            {
                // если перевернутая фигура уже есть в разновидностях, значит, и все варианты ее поворотов уже добавлены
                return kinds;
            }

            kinds.Add(turned);

            var turnedAndRotated90 = turned.GetRotated90();
            if (!kinds.Contains(turnedAndRotated90))
                kinds.Add(turnedAndRotated90);

            var turnedAndRotated180 = turnedAndRotated90.GetRotated90();
            if (!kinds.Contains(turnedAndRotated180))
                kinds.Add(turnedAndRotated180);

            var turnedAndRotated270 = turnedAndRotated180.GetRotated90();
            if (!kinds.Contains(turnedAndRotated270))
                kinds.Add(turnedAndRotated270);

            return kinds;
        }
    }
}
