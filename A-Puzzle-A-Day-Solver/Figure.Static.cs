using System;

namespace A_Puzzle_A_Day_Solver
{
    /// <summary>
    /// Фигура
    /// </summary>
    public partial class Figure : IEquatable<Figure>
    {
        static Figure()
        {
            // проверка количества фигур, т.к. на это завязан код в PlacementFinder
            if (Bundle.Length != 8)
                throw new Exception("Фигур в наборе должно быть 8, а не " + Bundle.Length);
        }

        /// <summary>
        /// Стандартный набор фигур из головоломки
        /// </summary>
        public static readonly Figure[] Bundle = {
            new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0),
            new Figure(
                1, 1, 1, 0,
                1, 0, 1, 0,
                0, 0, 0, 0,
                0, 0, 0, 0),
            new Figure(
                0, 1, 1, 0,
                0, 1, 0, 0,
                1, 1, 0, 0,
                0, 0, 0, 0),
            new Figure(
                1, 1, 1, 1,
                0, 1, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0),
            new Figure(
                0, 0, 1, 1,
                1, 1, 1, 0,
                0, 0, 0, 0,
                0, 0, 0, 0),
            new Figure(
                1, 1, 1, 1,
                1, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0),
            new Figure(
                1, 1, 0, 0,
                1, 1, 1, 0,
                0, 0, 0, 0,
                0, 0, 0, 0),
            new Figure(
                1, 1, 1, 0,
                1, 1, 1, 0,
                0, 0, 0, 0,
                0, 0, 0, 0)
        };
    }
}
