using System;
using System.Collections.Generic;
using A_Puzzle_A_Day_Solver;
using NUnit.Framework;

namespace A_Puzzle_A_Day.Tests
{
    public class DeskTests
    {
        [Test]
        public void CanPlaceFigure()
        {
            var desk = Desk.Create();

            var figure = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            Assert.IsTrue(Desk.CanPlaceFigure(desk, figure, 0, 0));
            Assert.IsTrue(Desk.CanPlaceFigure(desk, figure, 1, 1));
            Assert.IsFalse(Desk.CanPlaceFigure(desk, figure, 0, 4));
            Assert.IsTrue(Desk.CanPlaceFigure(desk, figure, 4, 0));
            Assert.IsTrue(Desk.CanPlaceFigure(desk, figure, 4, 2));
            Assert.IsFalse(Desk.CanPlaceFigure(desk, figure, 4, 3));
        }

        [Test]
        public void PlaceFigure()
        {
            var desk = Desk.Create();

            var figure = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            desk = Desk.PlaceFigure(desk, figure, 0, 0);
            Assert.IsFalse(Desk.CanPlaceFigure(desk, figure, 0, 0));
            Assert.IsTrue(Desk.CanPlaceFigure(desk, figure, 1, 1));
        }

        //[Test]
        //public void Test_ToString()
        //{
        //    var desk = Desk.Create();

        //    var figure = new Figure(
        //        1, 1, 1, 0,
        //        1, 0, 0, 0,
        //        1, 0, 0, 0,
        //        0, 0, 0, 0);

        //    var placements = new[]
        //    {
        //        figure.GetMask(0, 0),
        //        figure.GetMask(2, 2)
        //    };

        //    for (var i = 0; i < placements.Length; i++)
        //        desk.PlaceFigure(placements[i]);

        //    Console.WriteLine(Desk.ToString(placements));
        //}

        [Test]
        public void TryGetMonthCell()
        {
            // январь
            Assert.IsTrue(Desk.TryGetMonthCell(1, out var row, out var column));
            Assert.AreEqual(0, row);
            Assert.AreEqual(0, column);

            // ноябрь
            Assert.IsTrue(Desk.TryGetMonthCell(11, out row, out column));
            Assert.AreEqual(1, row);
            Assert.AreEqual(4, column);

            // несуществующий месяц
            Assert.IsFalse(Desk.TryGetMonthCell(0, out _, out _));
            Assert.IsFalse(Desk.TryGetMonthCell(13, out _, out _));
        }

        [Test]
        public void TryGetDayCell()
        {
            // 1-е число
            Assert.IsTrue(Desk.TryGetDayCell(1, out var row, out var column));
            Assert.AreEqual(2, row);
            Assert.AreEqual(0, column);

            // 11-е число
            Assert.IsTrue(Desk.TryGetDayCell(11, out row, out column));
            Assert.AreEqual(3, row);
            Assert.AreEqual(3, column);
        }
    }
}
